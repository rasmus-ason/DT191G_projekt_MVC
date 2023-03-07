using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using Newtonsoft.Json;

namespace DT191G_projekt.Controllers
{
    public class DetailedOrderController : Controller
    {
        private readonly DetailedOrderContext _context;
        private readonly ProductContext _productContext;
        private readonly ILogger<DetailedOrderController> _logger;


        public DetailedOrderController(DetailedOrderContext context, ProductContext productContext, ILogger<DetailedOrderController> logger)
        {
            _context = context;
            _productContext = productContext;
            _logger = logger;

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
        public async Task<IActionResult> Create([FromBody] DetailedOrder detailedOrderbody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var detailedOrder = new DetailedOrder
                {
                    OrderNumber = detailedOrderbody.OrderNumber,
                    Articles = new List<Article>()
                };

                foreach (var articleDto in detailedOrderbody.Articles)
                {
                    var article = new Article
                    {
                        ArticleNumber = articleDto.ArticleNumber,
                        Amount = articleDto.Amount
                    };
                    detailedOrder.Articles.Add(article);
                }

                _context.DetailedOrder.Add(detailedOrder);
                await _context.SaveChangesAsync();

                return Ok(detailedOrder.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the order");
            }
        }


        [HttpGet("getpackinglist/{ordernumber}")]
        public async Task<ActionResult<List<Article>>> GetPackingList(int ordernumber)
        {
            //Get orderId from the ordernumber
            var getOrderIdFromOrdernumber = await _context.DetailedOrder.Where(o => o.OrderNumber == ordernumber).FirstOrDefaultAsync();

            if (getOrderIdFromOrdernumber == null) 
            {
                return NotFound();
            }

            //Extract orderId from selected row
            var uniqueOrderId = getOrderIdFromOrdernumber.Id;

            //Get all rows with selected orderId
            var articles = await _context.Articles
                .Where(a => a.DetailedOrderId == uniqueOrderId)
                .ToListAsync();

            // Store the product titles in the ViewBag
            ViewBag.Ordernumber = ordernumber;
            ViewBag.PackingList = articles;


            return View(articles);
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
