using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using Microsoft.Extensions.Logging;


namespace DT191G_projekt.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly CustomerOrderContext _context;
        private readonly ILogger<CustomerOrderController> _logger;

        public CustomerOrderController(CustomerOrderContext context, ILogger<CustomerOrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CustomerOrder - Get orders that is not packed or shipped
        public async Task<IActionResult> Index()
        {
              //Check that IsPacked & isShipped is set to false
                var orders = await _context.CustomerOrder
                    .Where(o => o.IsPacked == false)
                    .Where(o => o.IsShipped == false)
                    .ToListAsync();

                //Return orders it's not null
                return orders != null ? View(orders) :
                Problem("Entity set 'CustomerOrderContext.CustomerOrder'  is null.");
        }

        // GET: CustomerOrder/PackedOrders - Get orders that is packed but not shipped
        public async Task<IActionResult> PackedOrders()
        {
            //Check that IsPacked is also set to true 
            var orders = await _context.CustomerOrder
                .Where(o => o.IsPacked == true)
                .Where(o => o.IsShipped == false)
                .ToListAsync();

            //Return orders it's not null
            return orders != null ? View(orders) :
            Problem("Entity set 'CustomerOrderContext.CustomerOrder'  is null.");
        }

        // GET: CustomerOrder/ShippedOrders - Get orders that is packed and shipped
        public async Task<IActionResult> ShippedOrders()
        {
            //Check that IsPacked and IsShipped is also set to true 
            var orders = await _context.CustomerOrder
                .Where(o => o.IsPacked == true)
                .Where(o => o.IsShipped == true)
                .ToListAsync();

            //Return orders it's not null
            return orders != null ? View(orders) :
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
        public async Task<IActionResult> Create([FromBody] CustomerOrder customerOrder)
        {
            //The body from the post req gets deserialized of the type CustomerOrder
            // Checks if the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Add DetailedOrder to context
                _context.Add(customerOrder);
                await _context.SaveChangesAsync();

                return Ok(customerOrder);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error message
                _logger.LogError(ex, "An error occurred while creating the order");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the order");
            }
        }

        //Post request that change bool value on isPacked/isShipped
        [HttpPost("changestatus/{ordernumber}")]
        public async Task<IActionResult> ChangeStatus(int? ordernumber)
        {
            if (ordernumber == null || _context.CustomerOrder == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder.FirstOrDefaultAsync(o => o.OrderNumber == ordernumber);

            if (customerOrder == null)
            {
                return NotFound();
            }


            if(customerOrder.IsPacked == false && customerOrder.IsShipped == false ){
                customerOrder.IsPacked = true;
            }

            if(customerOrder.IsPacked == true && customerOrder.IsShipped == false ){
                customerOrder.IsShipped = true;
            }

            _context.CustomerOrder.Update(customerOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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
