using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetApplication.Models;
using System.Security.Claims;

namespace NetApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterModel newUser)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("valid");
                var user = new Users()
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = newUser.Email,
                };
                var result = await _userManager.CreateAsync(user, newUser.Password);
                Console.WriteLine(result);
                if (result.Succeeded)
                {
                    Console.WriteLine("success");
                    await _signInManager.PasswordSignInAsync(user, newUser.Password, false, false);
                    TempData["success"] = "You've successfully registered!!";
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(newUser);
                }
            }
            else
            {
                Console.WriteLine("invalid");
                return View(newUser);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,false, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("success");
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("Username", model.Username);
                    return RedirectToAction("Index", "Profile");
                 }
                 else
                 {
                    TempData["loginError"] = "Invalid password";
                    return View(model);
                 }
             }
             else
                {
                TempData["userError"] = "User not found";
                return View(model);

                }
         }


        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);

                    if (result.Succeeded)
                    {
                        TempData["passChange"] = "Password changed successfully. Login again to continue";
                        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["passFail"] = "Current password is wrong!!";
                    }
                }
                else
                {
                    TempData["noUser"] = "No user found";
                }
            }

            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
