using EGames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGames.Controllers
{
    public class UserDetailController : Controller
    {
        private readonly DemoContext _context;



        public IEnumerable<object> OrderMasters { get; private set; }



        public UserDetailController(DemoContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {



            var GameContext = _context.users;
            return View(await GameContext.ToListAsync());



        }
    }
}
