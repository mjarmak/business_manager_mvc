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
    [Produces("applications/json")]
    [Route("users")]
    [ApiController]
    public class UserAccountModelsController : ControllerBase
    {
        private readonly DefaultContext _context;

        public UserAccountModelsController(DefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// List of all users
        /// </summary>
        /// <returns>It return the user accounts</returns>
        // GET: api/UserAccountModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountModel>>> GetUserAccount()
        {
            return await _context.UserAccount.ToListAsync();
        }
        /// <summary>
        /// Find in the list the user account base on the id
        /// </summary>
        /// <param name="id">id = 0</param>
        /// <returns></returns>
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

        /// <summary>
        /// Update user account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userAccountModel"></param>
        /// <returns>user details</returns>
        // PUT: api/UserAccountModels/5
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

        /// <summary>
        /// Send user details
        /// </summary>
        /// <param name="userAccountModel"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Sample request:
        ///   POST /
        ///   {
        ///		"Id": 1,
        ///     "Name" : "pippo",
        ///		"Surname" : "Pluto"
        ///   }
        /// </remarks>
        ///  <response code="201">Return the newfly created item</response>
        ///  <response code="400">If the element is null</response>
        /// POST: api/UserAccountModels
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserAccountModel>> PostUserAccountModel(UserAccountModel userAccountModel)
        {
            _context.UserAccount.Add(userAccountModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAccountModel", new { id = userAccountModel.Id }, userAccountModel);
        }
        /// <summary>
        /// Delete a speific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User deleted</returns>
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
