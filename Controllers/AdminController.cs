using EGames.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace EGames.Controllers
{
    public class AdminController : Controller
    {
        private readonly DemoContext db;
        private readonly ISession session;
        public AdminController(DemoContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin obj)
        {
            var result = (from i in db.admins
                          where i.Gmail == obj.Gmail && i.Password == obj.Password
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("Username", result.Name);
                return RedirectToAction("Index", "Products");
            }
            else
                return View();
        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}

