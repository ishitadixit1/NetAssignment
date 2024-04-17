using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApplication.Data;
using NetApplication.Models;

namespace NetApplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var Id = _userManager.GetUserId(HttpContext.User);
            var profiles = _context.Profiles.Where(n => n.UserId == Id).ToList();
            return View(profiles);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Profiles profile)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var newProfile = new Profiles()
                {
                    Name = profile.Name,
                    Link = profile.Link,
                    UserId = userId
                };
                _context.Profiles.Add(newProfile);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var profile = _context.Profiles.FirstOrDefault(n => n.Id == id);
            if (profile.UserId == userId)
            {
                var newProfile = new Profiles()
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    Link = profile.Link,
                    UserId = userId,
                };
                return View(newProfile);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Profiles profile)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                if (profile.UserId == userId)
                {
                    var newProfile = new Profiles()
                    {  
                        Id = profile.Id,
                        Name = profile.Name,
                        Link = profile.Link,
                        UserId = userId
                    };
                    _context.Profiles.Update(newProfile);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(profile);
        }

        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var newProfile = _context.Profiles.FirstOrDefault(n => n.Id == id);
            if (newProfile.UserId == userId)
            {

                _context.Profiles.Remove(newProfile);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }




    }
}
