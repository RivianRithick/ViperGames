using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EGames.Models;

namespace EGames.Controllers
{
    public class CartController : Controller
    {
        private readonly DemoContext _context;

        public CartController(DemoContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var demoContext = _context.carts.Include(c => c.product).Include(c => c.user);
            return View(await demoContext.ToListAsync());
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .Include(c => c.product)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            //ViewData["Productid"] = new SelectList(_context.products, "Id", "Id");
            //ViewData["Userid"] = new SelectList(_context.users, "Id", "Id");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Cart cart)
        {
            cart.Productid =(int?)HttpContext.Session.GetInt32("ProductId");
            cart.Userid = (int?)HttpContext.Session.GetInt32("Id");
            //int cost= (int)HttpContext.Session.GetInt32("price");
            //cart.TotalAmt = cart.Quantity * cost;
            if (ModelState.IsValid)
            {
                int cost = (int)HttpContext.Session.GetInt32("Price");
                cart.TotalAmt = cart.Quantity * cost;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Productid"] = new SelectList(_context.products, "Id", "Id", cart.Productid);
            //ViewData["Userid"] = new SelectList(_context.users, "Id", "Id", cart.Userid);
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.products, "Id", "Id", cart.Productid);
            ViewData["Userid"] = new SelectList(_context.users, "Id", "Id", cart.Userid);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,Quantity,TotalAmt,Productid,Userid")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.products, "Id", "Id", cart.Productid);
            ViewData["Userid"] = new SelectList(_context.users, "Id", "Id", cart.Userid);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .Include(c => c.product)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.carts == null)
            {
                return Problem("Entity set 'DemoContext.carts'  is null.");
            }
            var cart = await _context.carts.FindAsync(id);
            if (cart != null)
            {
                _context.carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ProceedtoBuy()
        {
            var UserId = HttpContext.Session.GetInt32("Id");
            List<Cart> cart = (from i in _context.carts where i.Userid == UserId select i).ToList();
            List<OrderDetails> od = new List<OrderDetails>();

            OrderMaster om = new OrderMaster();



            om.Date = DateTime.Today;
            om.UserId = (int)UserId;
            om.TotalAmount = 0;
            foreach (var item in cart)
            {



                om.TotalAmount += (int)item.TotalAmt;
            }
            _context.Add(om);



            _context.SaveChanges();
            HttpContext.Session.SetInt32("Total", (int)om.TotalAmount);
            foreach (var item in cart)
            {
                OrderDetails detail = new OrderDetails();
                detail.ProductId = item.Productid;
                detail.UserId = item.Userid;
                detail.TotalAmount = item.TotalAmt;
                detail.OrderMasterId = om.OrderMasterId;
                od.Add(detail);
            }
            _context.AddRange(od);
            _context.SaveChanges();





            return RedirectToAction("GetPayment", new { id = om.OrderMasterId });



        }
        public IActionResult GetPayment(int id)
        {
            var OrderMaster = _context.OrderMaster.Find(id);
            return View(OrderMaster);
        }




        [HttpPost]
        public IActionResult GetPayment(OrderMaster m)
        {




            if (m.AmountPaid == m.TotalAmount)
            {
                var UserId = HttpContext.Session.GetInt32("Userid");
                List<Cart> cart = (from i in _context.carts where i.Userid == UserId select i).ToList();
                var pid = (int)HttpContext.Session.GetInt32("Id");

                Product p = new Product();
                var s = (from i in _context.products
                         where i.Id == pid
                         select i).SingleOrDefault();
                var c = (from t in _context.carts
                         where t.Productid == pid
                         select t).SingleOrDefault();
                _context.OrderMaster.Update(m);
                _context.SaveChanges();
                _context.products.Update(s);
                _context.SaveChanges();
                _context.carts.RemoveRange(cart);
                _context.SaveChanges();




                return RedirectToAction("Thankyou");
            }
            else
            {
                ViewBag.ErrorMessage = "amount not valid";
                return View(m);
            }



        }

        public IActionResult Thankyou()
        {
            return View();
        }

        private bool CartExists(int id)
        {
          return _context.carts.Any(e => e.CartId == id);
        }
    }
}
