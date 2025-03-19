using Microsoft.AspNetCore.Mvc;
using compito_17_03.Models;
using compito_17_03.ViewModels;
using compito_17_03.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace compito_17_03.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var newUser = new ApplicationUser()
            {
                Nome = registerViewModel.Nome,
                Cognome = registerViewModel.Cognome,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                DataNascita = registerViewModel.DataNascita,
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            //await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel logInViewModel) 
        {
            var user = await _userManager.FindByEmailAsync(logInViewModel.Email);

            if (user == null)
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, logInViewModel.Password, true, false);

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, $"{user.Nome} {user.Cognome}"));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (!signInResult.Succeeded)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
