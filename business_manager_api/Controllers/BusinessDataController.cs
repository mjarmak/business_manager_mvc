using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using business_manager_common_library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace business_manager_api.Controllers
{
    [Produces("application/json")]
    [Route("business")]
    [ApiController]
    //[Authorize]
    public class BusinessDataController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public BusinessDataController(DefaultContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> GetBusinessDataModel()
        {
            return Ok(new
            {
                //status = response.StatusCode,
                data = await _context.BusinessDataModel
                .Include(b => b.BusinessInfo)
                .Include(b => b.BusinessInfo.Address)
                .Include(b => b.Identification)
                .ToListAsync()
            });
        }

        // GET: api/BusinessData/5
        [HttpGet("{id}")]
        public ActionResult GetBusinessDataModel(long id)
        {
            BusinessDataModel businessDataModel;
            try {
                businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
                    .Single(b => b.Id == id);
            } catch (InvalidOperationException e)
            {
                return NotFound();
            }

            if (businessDataModel == null)
            {
                return NotFound();
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
        /// <param name="businessDataModel"></param>
        /// <returns>The information of a business based on the id</returns>
        // PUT: api/BusinessData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessDataModel(long id, BusinessDataModel businessDataModel)
        {
            if (id != businessDataModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessDataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessDataModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
        public async Task<ActionResult<BusinessDataModel>> PostBusinessDataModel(BusinessModel businessModel)
        {
            BusinessDataModel businessDataModel = new BusinessDataModel
            {
                WorkHours = businessModel.WorkHours,
                Identification = new IdentificationData
                {
                    Description = businessModel.Identification.Description,
                    EmailPro = businessModel.Identification.EmailPro,
                    Name = businessModel.Identification.Name,
                    TVA = businessModel.Identification.TVA,
                    Type = businessModel.Identification.Type
                },
                BusinessInfo = new BusinessInfoData
                {
                    EmailBusiness = businessModel.BusinessInfo.EmailBusiness,
                    Phone = businessModel.BusinessInfo.Phone,
                    UrlFaceBook = businessModel.BusinessInfo.UrlFaceBook,
                    UrlInstagram = businessModel.BusinessInfo.UrlInstagram,
                    UrlLinkedIn = businessModel.BusinessInfo.UrlLinkedIn,
                    UrlSite = businessModel.BusinessInfo.UrlSite,
                    Address = new AddressData
                    {
                        BoxNumber = businessModel.BusinessInfo.Address.BoxNumber,
                        City = businessModel.BusinessInfo.Address.City,
                        Country = businessModel.BusinessInfo.Address.Country,
                        PostalCode = businessModel.BusinessInfo.Address.PostalCode,
                        Street = businessModel.BusinessInfo.Address.Street
                    }
                }
            };

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

            if (!BusinessDataModelExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!image.ContentType.Contains("image") && !image.ContentType.Contains("jpeg") && !image.ContentType.Contains("jpg"))
            {
                return BadRequest("File must be an image");
            }

            string imagesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            if (image != null) {
                string uniqueLogoName = Guid.NewGuid().ToString() + "_logo_" + image.FileName;
                string filePath = Path.Combine(imagesFolder, uniqueLogoName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));

                BusinessDataModel businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
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
                return BadRequest("Image ID must be between 1 and 5 inclusive");
            }
            if (!BusinessDataModelExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!image.ContentType.Contains("image") && !image.ContentType.Contains("jpeg") && !image.ContentType.Contains("jpg"))
            {
                return BadRequest("File must be an image");
            }

            string imagesFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
            if (image != null) {
                string uniqueLogoName = Guid.NewGuid().ToString() + "_photo_" + image.FileName;
                string filePath = Path.Combine(imagesFolder, uniqueLogoName);
                image.CopyTo(new FileStream(filePath, FileMode.Create));

                BusinessDataModel businessDataModel = _context.BusinessDataModel
                    .Include(b => b.BusinessInfo)
                    .Include(b => b.BusinessInfo.Address)
                    .Include(b => b.Identification)
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
                return NotFound();
            }

            _context.BusinessDataModel.Remove(businessDataModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = businessDataModel
            });
        }

        private bool BusinessDataModelExists(long id)
        {
            return _context.BusinessDataModel.Any(e => e.Id == id);
        }
    }
}
