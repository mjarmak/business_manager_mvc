using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using business_manager_api;

namespace business_manager_api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserAccountModelsController : ControllerBase
    {
        private readonly DefaultContext _context;

        public UserAccountModelsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/UserAccountModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountModel>>> GetUserAccount()
        {
            return await _context.UserAccount.ToListAsync();
        }

        // GET: api/UserAccountModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountModel>> GetUserAccountModel(long id)
        {
            var userAccountModel = await _context.UserAccount.FindAsync(id);

            if (userAccountModel == null)
            {
                return NotFound();
            }

            return userAccountModel;
        }

        // PUT: api/UserAccountModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccountModel(long id, UserAccountModel userAccountModel)
        {
            if (id != userAccountModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAccountModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountModelExists(id))
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

        // POST: api/UserAccountModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserAccountModel>> PostUserAccountModel(UserAccountModel userAccountModel)
        {
            _context.UserAccount.Add(userAccountModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAccountModel", new { id = userAccountModel.Id }, userAccountModel);
        }

        // DELETE: api/UserAccountModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAccountModel>> DeleteUserAccountModel(long id)
        {
            var userAccountModel = await _context.UserAccount.FindAsync(id);
            if (userAccountModel == null)
            {
                return NotFound();
            }

            _context.UserAccount.Remove(userAccountModel);
            await _context.SaveChangesAsync();

            return userAccountModel;
        }

        private bool UserAccountModelExists(long id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }
    }
}
