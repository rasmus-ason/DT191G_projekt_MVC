using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using Microsoft.AspNetCore.Authorization;

namespace DT191G_projekt.Controllers
{
    [Authorize]
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryContext _context;
        private readonly ProductContext _productContext;

        public ProductCategoryController(ProductCategoryContext context, ProductContext productContext)
        {
            _context = context;
            _productContext = productContext;
        }

        // GET: ProductCategory
        public async Task<IActionResult> Index()
        {
              return _context.ProductCategory != null ? 
                          View(await _context.ProductCategory.ToListAsync()) :
                          Problem("Entity set 'ProductCategoryContext.ProductCategory'  is null.");
        }

        // GET: ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductCategory == null)
            {
                var message = new { error = "Id/Context did not exist" };
                return NotFound(new JsonResult(message));
            }

            var productCategory = await _context.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                var message = new { error = "Product category not found" };
                return NotFound(new JsonResult(message));
            }
            return View(productCategory);
        }

        // POST: ProductCategory/Edit/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] ProductCategory productCategory)
        {
            if (id != productCategory.CategoryId)
            {
                var message = new { error = "Id not match" };
                return NotFound(new JsonResult(message));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.CategoryId))
                    {
                        var message = new { error = "Id not exist" };
                        return NotFound(new JsonResult(message));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductCategory == null)
            {
                var message = new { error = "Id/Context did not exist" };
                return NotFound(new JsonResult(message));
            }

            var productCategory = await _context.ProductCategory
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (productCategory == null)
            {
                var message = new { error = "Product category not exist" };
                return NotFound(new JsonResult(message));
            }

            return View(productCategory);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductCategory == null)
            {
                return Problem("Entity set 'ProductCategoryContext.ProductCategory'  is null.");
            }

            var productCategory = await _context.ProductCategory.FindAsync(id);

            if(productCategory != null) {

                //Check if category has products attached to it
                var hasProducts = await _productContext.Product.FirstOrDefaultAsync(p => p.Category == productCategory.CategoryName);

                if(hasProducts != null) {
                    ViewBag.CantRemoveCategory = "Kategorin kan ej raderas";
                    return View(productCategory);
                }else {
                    _context.ProductCategory.Remove(productCategory);
                }

            }

           
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
          return (_context.ProductCategory?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
