using EGames.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGames.Controllers
{
    public class MasterController : Controller
    {
        private readonly DemoContext _context;



        public IEnumerable<object> OrderMasters { get; private set; }



        public MasterController(DemoContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {



            var GameContext = _context.OrderMaster;
            return View(await GameContext.ToListAsync());



        }
        public async Task<IActionResult> Details(int? id)
        {

            ViewBag.Username = HttpContext.Session.GetString("Username");




            if (ViewBag.Username != null)
            {
                if (id == null || _context.OrderMaster == null)
                {
                    return NotFound();
                }



                var master = await _context.OrderMaster
                     .Include(p => p.User)
                     
                     .FirstOrDefaultAsync(m => m.OrderMasterId == id);
                if (master == null)
                {
                    return NotFound();
                }



                return View(master);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        
    }
}

