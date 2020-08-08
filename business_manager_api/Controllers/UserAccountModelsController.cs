using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using business_manager_common_library;
using FluentValidation.Results;
using business_manager_api.Context;
// ReSharper disable All

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

        [HttpGet("genders")]
        public ActionResult GetUserGenders()
        {
            var types = (from UserGenderEnum type in Enum.GetValues(typeof(UserGenderEnum)) select type.ToString()).ToList();
            return Ok(new
            {
                data = types
            });
        }

        [HttpGet("types")]
        public ActionResult GetUserTypes()
        {
            var types = (from UserTypeEnum type in Enum.GetValues(typeof(UserTypeEnum)) select type.ToString()).ToList();
            return Ok(new
            {
                data = types
            });
        }

        [HttpGet("states")]
        public ActionResult GetUserStates()
        {
            var types = (from UserStateEnum type in Enum.GetValues(typeof(UserStateEnum)) select type.ToString()).ToList();
            return Ok(new
            {
                data = types
            });
        }

        /// <summary>
        /// Get the list of all users
        /// </summary>
        /// <remarks>Get an array of all users</remarks>
        /// <response code="500">Internal Server Error</response>
        // GET: api/UserAccountModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountDataModel>>> GetUserAccount()
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
        public async Task<ActionResult<UserAccountDataModel>> GetUserAccountModel(long id)
        {
            var userAccountModel = await _context.UserAccount.FindAsync(id);

            if (userAccountModel == null)
            {
                return NotFound(new
                {
                    data = "User with ID " + id + " does not exist."
                });
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
        /// <returns>The information of a user based on the id</returns>
        // PUT: api/UserAccountModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccountModel(long id, UserAccountModel userAccountModel)
        {
            UserAccountDataModel userAccountDataModel;
            try
            {
                userAccountDataModel = EnvelopeOf(userAccountModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest("Invalid paramaters, " + e.Message);
            }
            var errors = ValidateUser();
            if (errors.Count() > 0)
            {
                return BadRequest(new
                {
                    //status = response.StatusCode,
                    data = errors
                });
            }
            _context.Entry(userAccountDataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountModelExists(id))
                {
                    return NotFound(new
                    {
                        data = "User with ID " + id + " does not exist."
                    });
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
        /// An example of a sample request:
        /// {
        ///    
        ///     "Id" : 1,
        ///     "Name" : "Marco",
        ///     "Surname" : "Rossi",
        ///     "Email" : "marco.rossi@info.com"
        /// }
        /// </remarks>
        /// <param name="userAccountModel"></param>
        /// <returns>A new user is created</returns>
        /// <response code="201">If the new user has been created</response>
        /// <response code="400">If the users field is null</response>
        /// <response code="403">If the users field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        // POST: api/UserAccountModels
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserAccountDataModel>> PostUserAccountModel(UserAccountModel userAccountModel)
        {

            UserAccountDataModel userAccountDataModel;
            try
            {
                userAccountDataModel = EnvelopeOf(userAccountModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    data = "Invalid paramaters, " + e.Message
                });
            }
            userAccountDataModel.State = UserStateEnum.REVIEWING.ToString();
            userAccountDataModel.Type = UserTypeEnum.USER.ToString();
            var errors = ValidateUser();
            if (errors.Count > 0)
            {
                return BadRequest(new
                {
                    //status = response.StatusCode,
                    data = errors
                });
            }

            await _context.UserAccount.AddAsync(userAccountDataModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = CreatedAtAction("GetUserAccountModel", new { id = userAccountDataModel.Id }, userAccountDataModel).Value
            });
        }

        /// <summary>
        /// Delete a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/UserAccountModels/5
        [ValidateAntiForgeryToken]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAccountDataModel>> DeleteUserAccountModel(long id)
        {
            var userAccountModel = await _context.UserAccount.FindAsync(id);
            if (userAccountModel == null)
            {
                return NotFound(new
                {
                    data = "User with ID " + id + " does not exist."
                });
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
        private static UserAccountDataModel EnvelopeOf(UserAccountModel userAccountModel)
        {
            return new UserAccountDataModel
            {
                Email = userAccountModel.Email,
                Name = userAccountModel.Name,
                Surname = userAccountModel.Surname,
                BirthDate = userAccountModel.BirthDate,
                Phone = userAccountModel.Phone,
                Profession = userAccountModel.Profession,
                Gender = userAccountModel.Gender == null ? null : ((UserGenderEnum)Enum.Parse(typeof(UserGenderEnum), userAccountModel.Gender)).ToString(),
            };
        }
        private static List<ValidationFailure> ValidateUser()
        {

            var errors = new List<ValidationFailure>();

            //UserAccountValidator userAccountValidator = new UserAccountValidator();
            //ValidationResult validationResult = userAccountValidator.Validate(userAccountDataModel);
            //errors.AddRange(validationResult.Errors);

            return errors;
        }
    }
}
