using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using business_manager_api;
using System.Net.Mime;

namespace business_manager_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessDataController : Controller
    {
        private readonly DefaultContext _context;

        public BusinessDataController(DefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get the the list of all bar, disco, etc...
        /// </summary>
        /// <returns>the list of building</returns>
        // GET: api/BusinessData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessDataModel>>> GetBusinessDataModel()
        {
            return await _context.BusinessDataModel.ToListAsync();
        }

        /// <summary>
        /// Find the details of a building
        /// </summary>
        /// <param name="id"></param>
        /// <returns>informations</returns>
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
        /// <summary>
        /// Update informations of a building
        /// </summary>
        /// <param name="id"></param>
        /// <param name="businessDataModel"></param>
        /// <returns></returns>
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
        /// Creation of a building
        /// </summary>
        /// <param name="businessDataModel"></param>
        /// <returns></returns>
        // POST: api/BusinessData
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BusinessDataModel>> PostBusinessDataModel(BusinessDataModel businessDataModel)
        {
            _context.BusinessDataModel.Add(businessDataModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessDataModel", new { id = businessDataModel.Id }, businessDataModel);
        }

        /// <summary>
        /// Delete the building
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
