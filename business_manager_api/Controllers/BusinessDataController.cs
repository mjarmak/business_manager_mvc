﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using business_manager_api.Context;
using business_manager_common_library;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace business_manager_api.Controllers
{
    [Produces("application/json")]
    [Route("business")]
    [ApiController]
    [Authorize]
    public class BusinessDataController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public BusinessDataController(DefaultContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("types")]
        public ActionResult GetBusinessTypes()
        {
            List<string> types = new List<string>();
            foreach (BusinessTypeEnum type in Enum.GetValues(typeof(BusinessTypeEnum)))
            {
                types.Add(type.ToString());
            }
            return Ok(new
            {
                //status = response.StatusCode,
                data = types
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> GetAllBusinesses()
        {
            return Ok(new
            {
                //status = response.StatusCode,
                data = await _context.BusinessDataModel
                .Include(b => b.BusinessInfo)
                .Include(b => b.BusinessInfo.Address)
                .Include(b => b.Identification)
                .Include(b => b.WorkHours)
                .ToListAsync()
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> SearchBusinesses([FromQuery] string type, [FromQuery] string country, [FromQuery] string city, [FromQuery] bool openNow)
        {
            var set = _context.BusinessDataModel
                .Include(b => b.BusinessInfo)
                .Include(b => b.BusinessInfo.Address)
                .Include(b => b.Identification)
                .Include(b => b.WorkHours)
                .AsQueryable();

            if (type != null)
            {
                set = set.Where(b => string.Equals(b.Identification.Type, type, StringComparison.OrdinalIgnoreCase));
            }
            if (city != null)
            {
                set = set.Where(b => string.Equals(b.BusinessInfo.Address.City, city, StringComparison.OrdinalIgnoreCase));
            }
            if (country != null)
            {
                set = set.Where(b => string.Equals(b.BusinessInfo.Address.Country, country, StringComparison.OrdinalIgnoreCase));
            }
            if (openNow)
            {
                DateTime today = DateTime.Now;
                string day = today.DayOfWeek.ToString().ToUpper();
                int hour = today.Hour;
                int minute = today.Minute;
                set = set.Where(b => b.WorkHours
                .Any(a => (
                    !a.Closed
                    && a.Day == day
                    && (a.HourFrom + (float)a.MinuteFrom/60) <= (hour + (float)minute/60)
                    && (a.HourTo + (float)a.MinuteTo/60) >= (hour + (float)minute/60)
                )));
            }

            return Ok(new
            {
                data = await set.ToListAsync()
            });
        }

        // GET: api/BusinessData/5
        [HttpGet("{id}")]
        public ActionResult GetBusinessDataModel(long id)
        {
            BusinessDataModel businessDataModel;
            try
            {
                businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
                    .Include(b => b.WorkHours)
                    .Single(b => b.Id == id);
            }
            catch (InvalidOperationException)
            {
                return NotFound(new
                {
                    data = "Business with ID " + id + " does not exist."
                });
            }

            if (businessDataModel == null)
            {
                return NotFound(new
                {
                    data = "Business with ID " + id + " does not exist."
                });
            }

            return Ok(new
            {
                //status = response.StatusCode,
                data = businessDataModel
            });
        }

        // GET: api/BusinessData/5
        [Route("page")]
        [HttpGet]
        public async Task<ActionResult> GetBusinessesByPage([FromQuery] int page, [FromQuery] int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var skip = (page - 1) * pageSize;
            var savedSearches = _context.BusinessDataModel.Skip(skip).Take(pageSize).Include(x => x);
            return Ok(new
            {
                //status = response.StatusCode,
                data = await savedSearches.ToArrayAsync()
            });
        }

        /// <summary>
        /// Update the business by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="businessModel"></param>
        /// <returns>The information of a business based on the id</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness(long id, BusinessModel businessModel)
        {
            if (id != businessModel.Id)
            {
                return BadRequest();
            }
            if (!BusinessExists(id))
            {
                return NotFound(new
                {
                    data = "Business with ID " + id + " does not exist."
                });
            }
            BusinessDataModel businessDataModel;
            try
            {
                businessDataModel = EnvelopeOf(businessModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest("Invalid paramaters, " + e.Message);
            }
            var errors = ValidateBusiness(businessDataModel);
            if (errors.Count() > 0)
            {
                return BadRequest(new
                {
                    //status = response.StatusCode,
                    data = errors
                }); ;
            }
            _context.Entry(businessDataModel).State = EntityState.Modified;
            if (businessDataModel.BusinessInfo != null && businessDataModel.BusinessInfo.Id != 0 && BusinessInfoExists(businessDataModel.BusinessInfo.Id))
            {
                _context.Entry(businessDataModel.BusinessInfo).State = EntityState.Modified;
                if (businessDataModel.BusinessInfo.Address != null && businessDataModel.BusinessInfo.Address.Id != 0 && AddressExists(businessDataModel.BusinessInfo.Address.Id))
                {
                    _context.Entry(businessDataModel.BusinessInfo.Address).State = EntityState.Modified;
                }
            }
            if (businessDataModel.Identification != null && businessDataModel.Identification.Id != 0 && IdentificaitonExists(businessDataModel.Identification.Id))
            {
                _context.Entry(businessDataModel.Identification).State = EntityState.Modified;
            }
            if (businessDataModel.WorkHours != null && businessDataModel.WorkHours.Count() > 0)
            {
                businessDataModel.WorkHours.ForEach(delegate (WorkHoursData workHours)
                {
                    if (workHours.Id != 0 && WorkHoursExists(workHours.Id))
                    {
                        _context.Entry(workHours).State = EntityState.Modified;
                    }
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Create a Business
        /// </summary>
        /// <remarks>
        /// {
        ///     "Id" : 1,
        ///     "Name" : "Mario",
        ///     "Surname" : "Rossi",
        ///     "Email" : "marco.rossi@info.com"
        /// }
        /// </remarks>
        /// <param name="businessModel"></param>
        /// <returns>A business entity</returns>
        /// <response code="201">If the new business has been created</response>
        /// <response code="400">If the business field is null</response>
        /// <response code="403">If the business field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>

        //Learned from https://csharp-video-tutorials.blogspot.com/2019/05/file-upload-in-aspnet-core-mvc.html
        [HttpPost]
        public async Task<ActionResult<BusinessDataModel>> CreateBusiness(BusinessModel businessModel)
        {
            BusinessDataModel businessDataModel;
            try
            {
                businessDataModel = EnvelopeOf(businessModel);
                businessDataModel.WorkHours = AddMissingDays(businessDataModel.WorkHours);
            }

            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    data = "Invalid paramaters, " + e.Message
                });
            }

            var errors = ValidateBusiness(businessDataModel);
            if (errors.Count() > 0)
            {
                return BadRequest(new
                {
                    data = errors
                }); ;
            }

            _context.BusinessDataModel.Add(businessDataModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = CreatedAtAction("GetBusinessDataModel", new { id = businessDataModel.Id }, businessDataModel).Value
            });
        }

        [HttpPost("force")]
        public async Task<ActionResult<BusinessDataModel>> PostBusinessDataModelForce(BusinessDataModel businessDataModel)
        {
            _context.BusinessDataModel.Add(businessDataModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = CreatedAtAction("GetBusinessDataModel", new { id = businessDataModel.Id }, businessDataModel).Value
            });
        }

        [HttpPost("{id}/logo")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(100_000_000)]
        public async Task<ActionResult> PostLogo(long id)
        {
            var image = Request.Form.Files[0];

            if (!BusinessExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!image.ContentType.Contains("image") && !image.ContentType.Contains("jpeg") && !image.ContentType.Contains("jpg"))
            {
                return BadRequest("File must be an image");
            }

            string imagesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            if (image != null)
            {
                string uniqueLogoName = Guid.NewGuid().ToString() + "_logo_" + image.FileName;
                string filePath = Path.Combine(imagesFolder, uniqueLogoName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));

                BusinessDataModel businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
                    .Include(b => b.WorkHours)
                    .Single(b => b.Id == id);
                businessDataModel.Identification.LogoPath = uniqueLogoName;
                _context.Entry(businessDataModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }


        [HttpPost("{id}/photo/{imageId}")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(100_000_000)]
        public async Task<ActionResult> PostPhoto(long id, long imageId)
        {
            var image = Request.Form.Files[0];

            if (imageId < 1 || imageId > 5)
            {
                return BadRequest(new
                {
                    data = "Image ID must be between 1 and 5 inclusive"
                });
            }
            if (!BusinessExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!image.ContentType.Contains("image") && !image.ContentType.Contains("jpeg") && !image.ContentType.Contains("jpg"))
            {
                return BadRequest(new
                {
                    data = "File must be an image"
                });
            }

            string imagesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            if (image != null)
            {
                string uniqueLogoName = Guid.NewGuid().ToString() + "_photo_" + image.FileName;
                string filePath = Path.Combine(imagesFolder, uniqueLogoName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));

                BusinessDataModel businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
                    .Include(b => b.WorkHours)
                    .Single(b => b.Id == id);
                switch (imageId)
                {
                    case 1:
                        businessDataModel.BusinessInfo.PhotoPath1 = uniqueLogoName;
                        break;
                    case 2:
                        businessDataModel.BusinessInfo.PhotoPath2 = uniqueLogoName;
                        break;
                    case 3:
                        businessDataModel.BusinessInfo.PhotoPath3 = uniqueLogoName;
                        break;
                    case 4:
                        businessDataModel.BusinessInfo.PhotoPath4 = uniqueLogoName;
                        break;
                    case 5:
                        businessDataModel.BusinessInfo.PhotoPath5 = uniqueLogoName;
                        break;
                }
                _context.Entry(businessDataModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a business by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/BusinessData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBusinessDataModel(long id)
        {
            var businessDataModel = await _context.BusinessDataModel.FindAsync(id);
            if (businessDataModel == null)
            {
                return NotFound(new
                {
                    data = "Business with ID " + id + " does not exist."
                });
            }

            _context.BusinessDataModel.Remove(businessDataModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                data = businessDataModel
            });
        }

        private bool BusinessExists(long id)
        {
            return _context.BusinessDataModel.Any(e => e.Id == id);
        }
        private bool WorkHoursExists(long id)
        {
            return _context.WorkHours.Any(e => e.Id == id);
        }
        private bool AddressExists(long id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
        private bool BusinessInfoExists(long id)
        {
            return _context.BusinessInfo.Any(e => e.Id == id);
        }
        private bool IdentificaitonExists(long id)
        {
            return _context.Identification.Any(e => e.Id == id);
        }
        private BusinessDataModel EnvelopeOf(BusinessModel businessModel)
        {
            return new BusinessDataModel
            {
                Id = businessModel.Id,
                WorkHours = businessModel.WorkHours == null ? null : businessModel.WorkHours.Select(workHours => new WorkHoursData()
                {
                    Id = workHours.Id,
                    Day = workHours.Day == null ? null : ((WorkHoursDayEnum)Enum.Parse(typeof(WorkHoursDayEnum), workHours.Day)).ToString(),
                    HourFrom = workHours.HourFrom,
                    HourTo = workHours.HourTo,
                    MinuteFrom = workHours.MinuteFrom,
                    MinuteTo = workHours.MinuteTo,
                    Closed = workHours.Closed
                }).ToList(),
                Identification = businessModel.Identification == null ? new IdentificationData() : new IdentificationData
                {
                    Id = businessModel.Identification.Id,
                    Description = businessModel.Identification.Description,
                    EmailPro = businessModel.Identification.EmailPro,
                    Name = businessModel.Identification.Name,
                    TVA = businessModel.Identification.TVA,
                    Type = businessModel.Identification.Type == null ? null : ((BusinessTypeEnum)Enum.Parse(typeof(BusinessTypeEnum), businessModel.Identification.Type)).ToString()
                },
                BusinessInfo = businessModel.BusinessInfo == null ? new BusinessInfoData{ Address = new AddressData() } : new BusinessInfoData
                {
                    Id = businessModel.BusinessInfo.Id,
                    EmailBusiness = businessModel.BusinessInfo.EmailBusiness,
                    Phone = businessModel.BusinessInfo.Phone,
                    UrlFaceBook = businessModel.BusinessInfo.UrlFaceBook,
                    UrlInstagram = businessModel.BusinessInfo.UrlInstagram,
                    UrlLinkedIn = businessModel.BusinessInfo.UrlLinkedIn,
                    UrlSite = businessModel.BusinessInfo.UrlSite,
                    Address = businessModel.BusinessInfo.Address == null ? new AddressData() : new AddressData
                    {
                        Id = businessModel.BusinessInfo.Address.Id,
                        BoxNumber = businessModel.BusinessInfo.Address.BoxNumber,
                        City = businessModel.BusinessInfo.Address.City,
                        Country = businessModel.BusinessInfo.Address.Country,
                        PostalCode = businessModel.BusinessInfo.Address.PostalCode,
                        Street = businessModel.BusinessInfo.Address.Street
                    }
                }
            };
        }

        private List<WorkHoursData> AddMissingDays(List<WorkHoursData> workHours)
        {
            foreach (WorkHoursDayEnum day in Enum.GetValues(typeof(WorkHoursDayEnum)))
            {
                if (!workHours.Any(w => w.Day == day.ToString())) {
                    workHours.Add(new WorkHoursData { Day = day.ToString(), Closed = true });
                }
            }
            return workHours;
        }

        private List<ValidationFailure> ValidateBusiness(BusinessDataModel businessDataModel)
        {

            List<ValidationFailure> errors = new List<ValidationFailure>();

            //BusinessDataValidator businessDataValidator = new BusinessDataValidator();
            //ValidationResult businessDataValidatorResult = businessDataValidator.Validate(businessDataModel);
            //errors.AddRange(businessDataValidatorResult.Errors);

            //BusinessInfoValidator businessInfoValidator = new BusinessInfoValidator();
            //ValidationResult businessInfoValidatorResult = businessInfoValidator.Validate(businessDataModel.BusinessInfo);
            //errors.AddRange(businessInfoValidatorResult.Errors);

            //IdentificationDataValidator identificationValidator = new IdentificationDataValidator();
            //ValidationResult identificationValidatorResult = identificationValidator.Validate(businessDataModel.Identification);
            //errors.AddRange(identificationValidatorResult.Errors);

            return errors;
        }
    }
}
