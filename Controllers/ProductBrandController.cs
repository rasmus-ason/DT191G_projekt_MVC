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
    public class ProductBrandController : Controller
    {
        private readonly ProductBrandContext _context;

        public ProductBrandController(ProductBrandContext context)
        {
            _context = context;
        }

        // GET: ProductBrand
        public async Task<IActionResult> Index()
        {
              return _context.ProductBrand != null ? 
                          View(await _context.ProductBrand.ToListAsync()) :
                          Problem("Entity set 'ProductBrandContext.ProductBrand'  is null.");
        }

        // GET: ProductBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductBrand == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrand
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return View(productBrand);
        }

        // GET: ProductBrand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }

        // GET: ProductBrand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductBrand == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrand.FindAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }
            return View(productBrand);
        }

        // POST: ProductBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName")] ProductBrand productBrand)
        {
            if (id != productBrand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBrandExists(productBrand.BrandId))
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
            return View(productBrand);
        }

        // GET: ProductBrand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductBrand == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrand
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return View(productBrand);
        }

        // POST: ProductBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductBrand == null)
            {
                return Problem("Entity set 'ProductBrandContext.ProductBrand'  is null.");
            }
            var productBrand = await _context.ProductBrand.FindAsync(id);
            if (productBrand != null)
            {
                _context.ProductBrand.Remove(productBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBrandExists(int id)
        {
          return (_context.ProductBrand?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
