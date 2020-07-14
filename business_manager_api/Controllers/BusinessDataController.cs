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
    [Route("business")]
    [ApiController]
    [Authorize]
    public class BusinessDataController : Controller
    {
        private readonly DefaultContext _context;

        public BusinessDataController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/BusinessData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> GetBusinessDataModel()
        {
            return await _context.BusinessDataModel.ToListAsync();
        }

        // GET: api/BusinessData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessDataModel>> GetBusinessDataModel(long id)
        {
            var businessDataModel = await _context.BusinessDataModel.FindAsync(id);

            if (businessDataModel == null)
            {
                return NotFound();
            }

            return businessDataModel;
        }

        // GET: api/BusinessData/5
        [Route("page")]
        [HttpGet]
        public async Task<IList<BusinessDataModel>> GetBusinessesByPage([FromQuery] int page, [FromQuery] int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var skip = (page - 1) * pageSize;
            var savedSearches = _context.BusinessDataModel.Skip(skip).Take(pageSize).Include(x => x);
            return await savedSearches.ToArrayAsync();
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

        // POST: api/BusinessData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BusinessDataModel>> PostBusinessDataModel(BusinessDataModel businessDataModel)
        {
            _context.BusinessDataModel.Add(businessDataModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessDataModel", new { id = businessDataModel.Id }, businessDataModel);
        }

        // DELETE: api/BusinessData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessDataModel>> DeleteBusinessDataModel(long id)
        {
            var businessDataModel = await _context.BusinessDataModel.FindAsync(id);
            if (businessDataModel == null)
            {
                return NotFound();
            }

            _context.BusinessDataModel.Remove(businessDataModel);
            await _context.SaveChangesAsync();

            return businessDataModel;
        }

        private bool BusinessDataModelExists(long id)
        {
            return _context.BusinessDataModel.Any(e => e.Id == id);
        }
    }
}
