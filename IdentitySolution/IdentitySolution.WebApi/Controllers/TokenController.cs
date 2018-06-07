using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentitySolution.Data;
using IdentitySolution.TokenHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySolution.WebApi.Controllers
{
    [Produces("application/json")]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TokenController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("/Token")]
        public async Task<IActionResult> CreateAsync([FromBody]LoginInputModel inputModel)
        {
            var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, false, false);

            if (!result.Succeeded)
            {
                //return Unauthorized();
                return Json(new
                {
                    Success = false,
                    Message = "Login Failed."
                });
            }

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create())
                                .AddSubject("token authentication")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                               // .AddClaim("MembershipId", "111")
                                .AddExpiry(3600)
                                .Build();

            //return Ok(token);
            return Json(new
            {
                Success = true,
                Message = "Login Successfully.",
                Token = token.Value,
                Validity = token.ValidTo
            });
        }

        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}