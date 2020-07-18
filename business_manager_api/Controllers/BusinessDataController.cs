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

        // GET: api/BusinessData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> GetBusinessDataModel()
        {
            return Ok(new
            {
                //status = response.StatusCode,
                data = await _context.BusinessDataModel.ToListAsync()
            });
        }

        // GET: api/BusinessData/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBusinessDataModel(long id)
        {
            var businessDataModel = await _context.BusinessDataModel.FindAsync(id);

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

        // PUT: api/BusinessData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

        //Learned from https://csharp-video-tutorials.blogspot.com/2019/05/file-upload-in-aspnet-core-mvc.html
        [HttpPost]
        public async Task<ActionResult<BusinessDataModel>> PostBusinessDataModel(BusinessModel businessModel)
        {
            BusinessDataModel businessDataModel = new BusinessDataModel
            {
                WorkHours = businessModel.WorkHours,
                IdentificationData = new IdentificationData
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
                data = CreatedAtAction("GetBusinessDataModel", new { id = businessModel.Id }, businessModel)
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

                BusinessDataModel businessDataModel = await _context.BusinessDataModel.FindAsync(id);
                businessDataModel.IdentificationData.LogoPath = uniqueLogoName;
                _context.BusinessDataModel.Add(businessDataModel);
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

                BusinessDataModel businessDataModel = await _context.BusinessDataModel.FindAsync(id);
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
                _context.BusinessDataModel.Add(businessDataModel);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

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
