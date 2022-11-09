using EGames.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGames.Controllers
{
    public class UserController : Controller
    {
        private readonly DemoContext db;
        private readonly ISession session;
        public UserController(DemoContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User obj)
        {
            var result = (from i in db.users
                          where i.Gmail == obj.Gmail && i.Password == obj.Password
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetInt32("Id", result.Id);
                HttpContext.Session.SetString("name", result.Name);
                return RedirectToAction("Index", "Products");
            }
            else
                return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User obj)
        {
            db.users.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Users()
        {
            var users = await db.users.ToListAsync();
            return View(users);
        }



    }
}

