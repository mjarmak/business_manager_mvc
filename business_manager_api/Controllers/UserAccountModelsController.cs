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
    [Produces("application/json")]
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserAccountModelsController : Controller
    {
        private readonly DefaultContext _context;

        public UserAccountModelsController(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the list of all users
        /// </summary>
        /// <remarks>Get an array of all users</remarks>
        /// <response code="500">Internal Server Error</response>
        // GET: api/UserAccountModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountModel>>> GetUserAccount()
        {
            return Ok(new
            {
                //status = response.StatusCode,
                data = await _context.UserAccount.ToListAsync()
            });
        }

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="id"></param>
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

            return Ok(new
            {
                //status = response.StatusCode,
                data = userAccountModel
            });
        }

        /// <summary>
        /// Update user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userAccountModel"></param>
        /// <returns></returns>
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
        /// Create a user
        /// </summary>
        /// <remarks>
        /// This is a sample request:
        /// {
        ///     "Id" : 1,
        ///     "Name" : "Marco",
        ///     "Surname" : "Rossi",
        ///     "Email" : "marco.rossi@info.com"
        /// }
        /// </remarks>
        /// <param name="userAccountModel"></param>
        /// <returns>A new user is created</returns>
        /// <response code="201">If the new user has been created</response>
        /// <response code="400">If the users fields is null</response>
        /// <response code="403">If the users field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        // POST: api/UserAccountModels
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserAccountModel>> PostUserAccountModel(UserAccountModel userAccountModel)
        {
            _context.UserAccount.Add(userAccountModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = CreatedAtAction("GetUserAccountModel", new { id = userAccountModel.Id }, userAccountModel)
            });
        }

        /// <summary>
        /// Delete a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            return Ok(new
            {
                //status = response.StatusCode,
                data = userAccountModel
            });
        }

        private bool UserAccountModelExists(long id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }
    }
}
