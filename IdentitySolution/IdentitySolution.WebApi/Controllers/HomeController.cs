using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentitySolution.Data;
using IdentitySolution.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySolution.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }


        [Route("/login")]
        public async Task<IActionResult> Login()
        {

            try
            {
                const string _adminRoleName = "Admin";
                string _adminEmail = "abh@narola.email";
                string _adminPassword = "Abh@123456";

                string[] _defaultRoles = new string[] { _adminRoleName, "Customer" };

                foreach (var role in _defaultRoles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var adminUsers = await _userManager.GetUsersInRoleAsync(_adminRoleName);

                if (!adminUsers.Any())
                {
                    var adminUser = new User()
                    {
                        Email = _adminEmail,
                        UserName = _adminEmail,
                        FirstName = "Abh",
                        LastName = "Narola",
                    };

                    var result = await _userManager.CreateAsync(adminUser, _adminPassword);
                    await _userManager.AddToRoleAsync(adminUser, _adminRoleName);
                }

            }
            catch (Exception ex)
            {
                var error = ex;
            }
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("Invalid login attempt");

                }
            }
            return View(model);
        }

    }
}
