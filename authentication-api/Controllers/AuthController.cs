using business_manager_common_library;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authentication_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
        [HttpPost]
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
            return Ok(new
            {
                data = result.ToString()
            });
        }
    }
}
