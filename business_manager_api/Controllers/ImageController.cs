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
        public async Task<ActionResult<BusinessImageModel>> GetBusinessImageModel(long id)
        {
            var businessImageModel = await _context.BusinessImage.FindAsync(id);

            if (businessImageModel == null)
            {
                return NotFound();
            }

            return businessImageModel;
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
            _context.BusinessImage.Add(new BusinessImageModel(id, imageString));
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
