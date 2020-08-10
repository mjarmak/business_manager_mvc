using business_manager_common_library;
using FluentValidation.Results;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace authentication_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        public SignInManager<IdentityUser> InManager { get; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public RoleManager<IdentityRole> RoleManager { get; }
        public IIdentityServerInteractionService InteractionService { get; }

        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            InManager = signInManager;
            _userManager = userManager;
            RoleManager = roleManager;
            InteractionService = interactionService;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Register a user.
        /// </summary>
        /// <param name="userAccountModel"></param>
        /// <returns>The user has been registered</returns>
        /// <response code="201">If the new user has been Registered</response>
        /// <response code="400">If the users field is null</response>
        /// <response code="403">If the users field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(UserAccountModel userAccountModel)
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
                    data = new List<string> { "Invalid paramaters, " + e.Message }
                });
            }
            var errors = ValidateUser(userAccountDataModel);
            if (errors.Count() > 0)
            {
                return BadRequest(new
                {
                    //status = response.StatusCode,
                    data = errors
                });
            }


            var user = new IdentityUser(userAccountDataModel.Email);
            var result = await _userManager.CreateAsync(user, userAccountDataModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            await _userManager.AddToRoleAsync(user, "REVIEWING");
            if (userAccountDataModel.Email != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, userAccountDataModel.Email));
            }
            if (userAccountDataModel.Name != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, userAccountDataModel.Name));
            }
            if (userAccountDataModel.Surname != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, userAccountDataModel.Surname));
            }
            if (userAccountDataModel.Gender != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, userAccountDataModel.Gender));
            }
            if (userAccountDataModel.Phone != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, userAccountDataModel.Phone));
            }
            if (userAccountDataModel.BirthDate != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, userAccountDataModel.BirthDate));
            }
            await _userManager.AddClaimAsync(user, new Claim( "Professional" , userAccountDataModel.Profession ? "1" : "0"));

            return Ok(new
            {
                data = result.ToString()
            });
        }

        /// <summary>
        /// Updates of the user infos
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userUpdateModel"></param>
        /// <returns>User info updated</returns>
        /// <response code="201">If the user info has been updated</response>
        /// <response code="400">If the users field is null</response>
        /// <response code="403">If the users field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        [Route("update/{email}")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "USER")]
        public async Task<ActionResult> Update(string email, UserUpdateModel userUpdateModel)
        {
            var emailClaim = GetClaim("email");
            if (emailClaim == null || !emailClaim.Equals(email))
            {
                return BadRequest(new
                {
                    data = new List<string> { "Invalid user. A user can only update his own info." }
                });
            }

            var errors = ValidateUser(userUpdateModel);
            if (errors.Count() > 0)
            {
                return BadRequest(new
                {
                    data = errors
                });
            }

            var user = await _userManager.FindByNameAsync(email);
            try
            {
                if (userUpdateModel.NameNew != null && userUpdateModel.NamePrevious != null)
                {
                    await _userManager.ReplaceClaimAsync(user,
                        new Claim(JwtClaimTypes.Name, userUpdateModel.NamePrevious),
                        new Claim(JwtClaimTypes.Name, userUpdateModel.NameNew));
                }
                if (userUpdateModel.SurnameNew != null && userUpdateModel.SurnamePrevious != null)
                {
                    await _userManager.ReplaceClaimAsync(user,
                        new Claim(JwtClaimTypes.FamilyName, userUpdateModel.SurnamePrevious),
                        new Claim(JwtClaimTypes.FamilyName, userUpdateModel.SurnameNew));
                }
                if (userUpdateModel.PhoneNew != null && userUpdateModel.PhonePrevious != null)
                {
                    await _userManager.ReplaceClaimAsync(user,
                        new Claim(JwtClaimTypes.PhoneNumber, userUpdateModel.PhonePrevious),
                        new Claim(JwtClaimTypes.PhoneNumber, userUpdateModel.PhoneNew));
                }
                await _userManager.ReplaceClaimAsync(user,
                    new Claim("Professional", userUpdateModel.ProfessionPrevious ? "1" : "0"),
                    new Claim("Professional", userUpdateModel.ProfessionNew ? "1" : "0"));

                return Ok(new
                {
                    data = "Succeed"
                });
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    data = new List<string> { "Failed, " + e.Message }
                });
            }
        }

        /// <summary>
        /// List of roles
        /// </summary>
        /// <returns></returns>
        /// <response code="201">If the role user has been created</response>
        /// <response code="400">If the role field is null</response>
        /// <response code="403">If the role field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        [Route("roles")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllRoles()
        {
            return Ok(new
            {
                data = RoleManager.Roles.ToList().Select(role => role.Name).ToList(),
            });
        }

        /// <summary>
        /// Show the list of users and change the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>It returns the list of users</returns>
        /// <response code="201">If the role user has been created</response>
        /// <response code="400">If the role field is null</response>
        /// <response code="403">If the role field is forbidden</response>
        /// <response code="500">If an internal server error occurred</response>
        [Route("user")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllUsers(string role)
        {
            if (role == null)
            {
                return Ok(new
                {
                    data = _userManager.Users
                });
            }

            return Ok(new
            {
                data = _userManager.GetUsersInRoleAsync(role).Result
            });
        }

        /// <summary>
        /// Search for the user based on role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("user/username")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> GetUsersByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            return Ok(new
            {
                data = user
            });
        }

        /// <summary>
        /// Give role type to user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("user/validate")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> ValidateUserAccount(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "BLOCKED");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            var result = await _userManager.AddToRoleAsync(user, "USER");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }

        /// <summary>
        ///  Remove role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("user/block")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> BlockUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "USER");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            var result = await _userManager.AddToRoleAsync(user, "BLOCKED");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }

        /// <summary>
        /// Remove role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("user/make-admin")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> MakeUserAdmin(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "BLOCKED");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            await _userManager.AddToRoleAsync(user, "USER");
            var result = await _userManager.AddToRoleAsync(user, "ADMIN");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountModel"></param>
        /// <returns></returns>
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
                Gender = userAccountModel.Gender == null ? null : ((UserGenderEnum) Enum.Parse(typeof(UserGenderEnum), userAccountModel.Gender)).ToString(),
                Password = userAccountModel.Password
            };
        }
        private static List<string> ValidateUser(UserAccountDataModel userAccountDataModel)
        {
            var errors = new List<ValidationFailure>();

            var userAccountValidator = new UserAccountValidator();
            var validationResult = userAccountValidator.Validate(userAccountDataModel);
            errors.AddRange(validationResult.Errors);

            return ErrorsToStrings(errors);
        }
        private static List<string> ValidateUser(UserUpdateModel userUpdateModel)
        {
            var errors = new List<ValidationFailure>();

            var userUpdateValidator = new UserUpdateValidator();
            var validationResult = userUpdateValidator.Validate(userUpdateModel);
            errors.AddRange(validationResult.Errors);

            return ErrorsToStrings(errors);
        }
        private static List<string> ErrorsToStrings(IEnumerable<ValidationFailure> validationFailures)
        {
            return validationFailures?.Select(validationFailure => validationFailure.ErrorMessage).ToList();
        }
        private string GetClaim(string name)
        {
            var accessTokenString = Request.Headers[HeaderNames.Authorization].ToString();

            if (accessTokenString == null || !accessTokenString.Contains("Bearer "))
            {
                return null;
            }

            try
            {
                var accessToken = _tokenHandler.ReadToken(accessTokenString.Replace("Bearer ", "")) as JwtSecurityToken;
                return accessToken.Claims.Single(claim => claim.Type == name).Value;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
