using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;

namespace DT191G_projekt.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly CustomerOrderContext _context;

        public CustomerOrderController(CustomerOrderContext context)
        {
            _context = context;
        }

        // GET: CustomerOrder
        public async Task<IActionResult> Index()
        {
              return _context.CustomerOrder != null ? 
                          View(await _context.CustomerOrder.ToListAsync()) :
                          Problem("Entity set 'CustomerOrderContext.CustomerOrder'  is null.");
        }

        // GET: CustomerOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerOrder == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

         // GET: /CustomerOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CustomerOrder customerOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customerOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(customerOrder);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }




        // GET: CustomerOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerOrder == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }
            return View(customerOrder);
        }

        // POST: CustomerOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderNumber,Firstname,Lastname,Email,Phonenumber,Adress,ZipCode,City,PurchaseDate,TotalPrice,ShippingCost,IsPacked,IsShipped")] CustomerOrder customerOrder)
        {
            if (id != customerOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderExists(customerOrder.OrderId))
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
            return View(customerOrder);
        }

        // GET: CustomerOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerOrder == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // POST: CustomerOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerOrder == null)
            {
                return Problem("Entity set 'CustomerOrderContext.CustomerOrder'  is null.");
            }
            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder != null)
            {
                _context.CustomerOrder.Remove(customerOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerOrderExists(int id)
        {
          return (_context.CustomerOrder?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
