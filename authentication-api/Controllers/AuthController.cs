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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public RoleManager<IdentityRole> _roleManager { get; }
        private readonly IIdentityServerInteractionService _interactionService;
        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _interactionService = interactionService;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

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
                }); ;
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
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, userAccountDataModel.BirthDate.ToString()));
            }
            await _userManager.AddClaimAsync(user, new Claim( "Professional" , userAccountDataModel.Profession ? "1" : "0"));

            return Ok(new
            {
                data = result.ToString()
            });
        }

        [Route("update/{email}")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "USER")]
        public async Task<ActionResult> Update(string email, UserUpdateModel userUpdateModel)
        {
            string emailClaim = GetClaim("email");
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
                }); ;
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


        [Route("roles")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllRoles()
        {
            return Ok(new
            {
                data = _roleManager.Roles.ToList().Select(role => role.Name).ToList(),
            });
        }

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

        private UserAccountDataModel EnvelopeOf(UserAccountModel userAccountModel)
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
        private List<string> ValidateUser(UserAccountDataModel userAccountDataModel)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            UserAccountValidator userAccountValidator = new UserAccountValidator();
            ValidationResult validationResult = userAccountValidator.Validate(userAccountDataModel);
            errors.AddRange(validationResult.Errors);

            return ErrorsToStrings(errors);
        }
        private List<string> ValidateUser(UserUpdateModel userUpdateModel)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            UserUpdateValidator userUpdateValidator = new UserUpdateValidator();
            ValidationResult validationResult = userUpdateValidator.Validate(userUpdateModel);
            errors.AddRange(validationResult.Errors);

            return ErrorsToStrings(errors);
        }
        private List<string> ErrorsToStrings(IList<ValidationFailure> validationFailures)
        {
            return validationFailures?.Select(ValidationFailure => ValidationFailure.ErrorMessage).ToList();
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
