using System;
using System.Threading.Tasks;
using AuthorizationExample.Auth.Models.Input;
using AuthorizationExample.Data.Dal.Entities.Identity;
using AuthorizationExample.Services.Authorization;
using AuthorizationExample.Services.Contracts.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationExample.Auth.Controllers
{
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AspNetUser> _userManager;

        private readonly SignInManager<AspNetUser> _signInManager;

        public AccountController(IUserManager<AspNetUser> userManager, ISignInManager<AspNetUser> signInManager)
        {
            _userManager = userManager as UserManager<AspNetUser>;
            _signInManager = signInManager as SignInManager<AspNetUser>;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserInputModel model)
        {
            var user = new AspNetUser
                       {
                           UserName = model.Email,
                           Email = model.Email,
                       };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}