using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using business_manager_api;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using business_manager_common_library;

namespace business_manager_api.Controllers
{
    [Route("image")]
    [ApiController]
    //[Authorize]
    public class ImageController : Controller
    {
        private readonly DefaultContext _context;

        public ImageController(DefaultContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessImageModel>>> GetBusinessImage()
        {
            return await _context.BusinessImage.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBusinessImageModel(long id)
        {
            var businessImageModel = await _context.BusinessImage.FindAsync(id);

            if (businessImageModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                //status = response.StatusCode,
                data = businessImageModel
            });
        }

        [HttpGet("{id}/image")]
        public async Task<ActionResult> GetBusinessImage(long id)
        {
            var businessImageModel = await _context.BusinessImage.FindAsync(id);

            if (businessImageModel == null)
            {
                return NotFound();
            }

            string imageStringBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(businessImageModel.ImageData));
            return Ok(new
            {
                //status = response.StatusCode,
                data = imageStringBase64
            });
        }

        [HttpGet("bussiness/{id}/image")]
        public ActionResult GetBusinessImages(long id)
        {
            List<BusinessImageModel> businessImageModel = _context.BusinessImage.Where(i => i.BusinessId == id).ToList();

            if (businessImageModel == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                //status = response.StatusCode,
                data = EnvelopeOfImages(businessImageModel)
            });
        }

        private List<ImageModel> EnvelopeOfImages(List<BusinessImageModel> BussinessImages)
        {
            List<ImageModel> Images = new List<ImageModel>();
            foreach (BusinessImageModel bi in BussinessImages)
            {
                Images.Add(new ImageModel(bi.Id, bi.BusinessId, System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(bi.ImageData))));
            }
            return Images;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessImageModel(long id, BusinessImageModel businessImageModel)
        {
            if (id != businessImageModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessImageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessImageModelExists(id))
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost()]
        public async Task<ActionResult<BusinessImageModel>> PostImage(BusinessImageModel businessImageModel)
        {
            _context.BusinessImage.Add(businessImageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessImageModel", new { id = businessImageModel.Id }, businessImageModel);
        }

        [HttpPost("business/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostBusinessImage(long id, IFormFile image)
        {
            if (!BusinessDataModelExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!image.ContentType.Contains("image") && !image.ContentType.Contains("jpeg") && !image.ContentType.Contains("jpg"))
            {
                return BadRequest("File must be an image");
            }
            Stream stream = image.OpenReadStream();
            StreamReader reader = new StreamReader(stream);
            string imageString = reader.ReadToEnd();
            string imageStringBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(imageString));
            _context.BusinessImage.Add(new BusinessImageModel(id, imageStringBase64));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Image/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessImageModel>> DeleteBusinessImageModel(long id)
        {
            var businessImageModel = await _context.BusinessImage.FindAsync(id);
            if (businessImageModel == null)
            {
                return NotFound();
            }

            _context.BusinessImage.Remove(businessImageModel);
            await _context.SaveChangesAsync();

            return businessImageModel;
        }

        private bool BusinessImageModelExists(long id)
        {
            return _context.BusinessImage.Any(e => e.Id == id);
        }
        private bool BusinessDataModelExists(long id)
        {
            return _context.BusinessDataModel.Any(e => e.Id == id);
        }
    }
}
