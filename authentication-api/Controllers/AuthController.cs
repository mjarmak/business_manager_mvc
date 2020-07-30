using business_manager_common_library;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace authentication_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RoleManager<IdentityRole> Manager { get; }
        private readonly IIdentityServerInteractionService _interactionService;
        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            Manager = roleManager;
            _interactionService = interactionService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginInfo loginInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(loginInfo.Username, loginInfo.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.ToString()
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }

        [Route("logout")]
        [HttpGet]
        public async Task<ActionResult> Logout(string logoutId)
        {
            return Ok();
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(LoginInfo loginInfo)
        {
            var user = new IdentityUser(loginInfo.Username);
            var result = await _userManager.CreateAsync(user, loginInfo.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.Errors
                });
            }
            var result2 = await _userManager.AddToRoleAsync(user, "REVIEWING");
            //await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, "REVIEW"));
            return Ok(new
            {
                data = result.ToString()
            });
        }

        [Route("user")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers(string role)
        {
            return Ok(new
            {
                data = _userManager.GetUsersInRoleAsync(role)
            });
        }
        [Route("user/username")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = "Bearer")]
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
                    data = result.Errors
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }
        [Route("user/block")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(Roles = "ADMIN")]
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
                    data = result.Errors
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }
    }
}
