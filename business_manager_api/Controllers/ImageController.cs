using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using business_manager_api;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessImageModel>>> GetBusinessImage()
        {
            return await _context.BusinessImage.ToListAsync();
        }

        // GET: api/Image/5
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

        // PUT: api/Image/5
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

        // POST: api/Image
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BusinessImageModel>> PostBusinessImageModel(BusinessImageModel businessImageModel)
        {
            _context.BusinessImage.Add(businessImageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessImageModel", new { id = businessImageModel.Id }, businessImageModel);
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
    }
}
