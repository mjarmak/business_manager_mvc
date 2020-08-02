﻿using business_manager_common_library;
using FluentValidation.Results;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                return BadRequest("Invalid paramaters, " + e.Message);
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


            var user = new IdentityUser(userAccountModel.Email);
            var result = await _userManager.CreateAsync(user, userAccountModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.Errors.ToList()[0].Description
                });
            }
            await _userManager.AddToRoleAsync(user, "REVIEWING");
            if (userAccountModel.Email != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, userAccountModel.Email));
            }
            if (userAccountModel.Name != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, userAccountModel.Name));
            }
            if (userAccountModel.Surname != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, userAccountModel.Surname));
            }
            if (userAccountModel.Gender != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, userAccountModel.Gender));
            }
            if (userAccountModel.Phone != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, userAccountModel.Phone));
            }
            if (userAccountModel.State != null) 
            {
                    await _userManager.AddClaimAsync(user, new Claim("State", userAccountModel.State));
            }
            if (userAccountModel.BirthDate != null)
            {
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, userAccountModel.BirthDate.ToString()));
            }
            await _userManager.AddClaimAsync(user, new Claim( "Professional" , userAccountModel.Profession ? "1" : "0"));

            return Ok(new
            {
                data = result.ToString()
            });
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
                    data = "User " + username + " doesn't exist"
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
        public async Task<ActionResult> ValidateUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = "User " + username + " doesn't exist"
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "BLOCKED");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            var result = await _userManager.AddToRoleAsync(user, "USER");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.Errors.ToList()[0].Description
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
        public async Task<ActionResult> BlockUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = "User " + userName + " doesn't exist"
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "USER");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            var result = await _userManager.AddToRoleAsync(user, "BLOCKED");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.Errors.ToList()[0].Description
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
                Id = userAccountModel.Id,
                Email = userAccountModel.Email,
                Name = userAccountModel.Name,
                Surname = userAccountModel.Surname,
                BirthDate = userAccountModel.BirthDate,
                Phone = userAccountModel.Phone,
                Profession = userAccountModel.Profession,
                Gender = userAccountModel.Gender == null ? null : ((UserGenderEnum) Enum.Parse(typeof(UserGenderEnum), userAccountModel.Gender)).ToString(),
                State = userAccountModel.State == null ? null : ((UserStateEnum) Enum.Parse(typeof(UserStateEnum), userAccountModel.State)).ToString(),
                Type = userAccountModel.Type == null ? null : ((UserTypeEnum) Enum.Parse(typeof(UserTypeEnum), userAccountModel.Type)).ToString()
            };
        }
        private List<ValidationFailure> ValidateUser(UserAccountDataModel userAccountDataModel)
        {
            List<ValidationFailure> errors = new List<ValidationFailure>();

            UserAccountValidator userAccountValidator = new UserAccountValidator();
            ValidationResult validationResult = userAccountValidator.Validate(userAccountDataModel);
            errors.AddRange(validationResult.Errors);

            return errors;
        }
    }
}
