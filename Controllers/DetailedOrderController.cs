using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using Newtonsoft.Json;

namespace DT191G_projekt.Controllers
{
    public class DetailedOrderController : Controller
    {
        private readonly DetailedOrderContext _context;

        public DetailedOrderController(DetailedOrderContext context)
        {
            _context = context;
        }

        // GET: DetailedOrder
        public async Task<IActionResult> Index()
        {
              return _context.DetailedOrder != null ? 
                          View(await _context.DetailedOrder.ToListAsync()) :
                          Problem("Entity set 'DetailedOrderContext.DetailedOrder'  is null.");
        }

        // GET: DetailedOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetailedOrder == null)
            {
                return NotFound();
            }

            var detailedOrder = await _context.DetailedOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailedOrder == null)
            {
                return NotFound();
            }

            return View(detailedOrder);
        }

        // GET: DetailedOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetailedOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] DetailedOrder json) 
        {
            if (ModelState.IsValid)
            {
                var detailedOrder = new DetailedOrder 
                {
                    OrderNumber = json.OrderNumber,
                    Articles = new List<Article>()
                };

                foreach (var articleJson in json.Articles)
                {
                    var article = new Article 
                    {
                        ArticleNumber = articleJson.ArticleNumber,
                        Amount = articleJson.Amount,
                        DetailedOrderId = detailedOrder.Id
                    };
                    detailedOrder.Articles.Add(article);
                }

                _context.Add(detailedOrder);
                await _context.SaveChangesAsync();
                return Ok("Order created successfully");
            }
            return BadRequest(ModelState);
        }


        // GET: DetailedOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetailedOrder == null)
            {
                return NotFound();
            }

            var detailedOrder = await _context.DetailedOrder.FindAsync(id);
            if (detailedOrder == null)
            {
                return NotFound();
            }
            return View(detailedOrder);
        }

        // POST: DetailedOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber")] DetailedOrder detailedOrder)
        {
            if (id != detailedOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detailedOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailedOrderExists(detailedOrder.Id))
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
            return View(detailedOrder);
        }

        // GET: DetailedOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetailedOrder == null)
            {
                return NotFound();
            }

            var detailedOrder = await _context.DetailedOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detailedOrder == null)
            {
                return NotFound();
            }

            return View(detailedOrder);
        }

        // POST: DetailedOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetailedOrder == null)
            {
                return Problem("Entity set 'DetailedOrderContext.DetailedOrder'  is null.");
            }
            var detailedOrder = await _context.DetailedOrder.FindAsync(id);
            if (detailedOrder != null)
            {
                _context.DetailedOrder.Remove(detailedOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailedOrderExists(int id)
        {
          return (_context.DetailedOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
