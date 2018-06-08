using System.Threading.Tasks;
using IdentitySolution.Data;
using IdentitySolution.TokenHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdentitySolution.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
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
        [AllowAnonymous]
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

            var user = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(user);

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create())
                                .AddSubject("token authentication")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111", roles)
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