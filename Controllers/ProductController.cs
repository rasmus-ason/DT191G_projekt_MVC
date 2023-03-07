using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;
using DT191G_projekt.Data;
using DT191G_projekt.Models;

namespace DT191G_projekt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        private readonly ProductCategoryContext _categoryContext;

        private readonly ProductBrandContext _brandContext;
        private readonly IWebHostEnvironment? _hostEnvironment;

        private string? wwwRootPath;

        public ProductController(
            ProductContext context, 
            ProductCategoryContext categoryContext, 
            ProductBrandContext brandContext,
            IWebHostEnvironment? hostEnvironment)
        {
            _context = context;
            _categoryContext = categoryContext;
            _brandContext = brandContext;
            wwwRootPath = _hostEnvironment?.WebRootPath;
            
        }

        // GET: Product
        [HttpGet("/Product")]
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ProductContext.Product'  is null.");
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Product.ToListAsync();

            if (products != null)
            {
                return Json(products);
            }
            else
            {
                return Problem("Entity set 'ProductContext.Product' is null.");
            }
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _context.Product.FindAsync(productId);

            if (product != null)
            {
                return Json(product);
            }
            else
            {
                return NotFound();
            }
        }



        // GET: Product/Details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
         // GET: Product/Create
         [HttpGet("Product/Create")]
        public IActionResult Create()
        {

            //Get product Categories
            var categories = _categoryContext.ProductCategory.ToArray();
            ViewBag.Categories = categories;

            //Get product brands
            var brands = _brandContext.ProductBrand.ToArray();
            ViewBag.Brands = brands;

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Product/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ArticleNumber,AmountInStock,Title,ProductInfo,AltText,Category,Weight,Price,Brand,Created,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {

                //Check if image was uploaded
                if(product.ImageFile != null) {

                    //Save images to the image folder
                    string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extention = Path.GetExtension(product.ImageFile.FileName);

                    //Remove blank spaces
                    product.ImageName = filename = filename.Replace(" ", String.Empty) + extention;

                    //Store the absolute path in Image Name
                    var wwwroot = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                    product.ImageName = wwwroot + "/wwwroot/imageupload/" + product.ImageName;

                    //Store path to save image in wwwroot/imageuploads
                    string path = Path.Combine(wwwRootPath + "wwwroot" + "/" + "imageupload", filename);

                    //Store file
                    using (var fileStream = new FileStream(path, FileMode.Create)){
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                   

                }else {
                    product.ImageName = null;
                }

                //Create and add a article number
                Random rnd = new Random();
                int articleNumber;
                bool articleNumberExists;

                do
                {
                    // Generate a new random 6-digit number
                    articleNumber = rnd.Next(100000, 999999);

                    // Check if the article number exists in the Product table
                    var p = _context.Product.FirstOrDefault(p => p.ArticleNumber == articleNumber);
                    // If the product variable is null, the article number doesn't exist
                    if (p == null)
                    {
                        articleNumberExists = false;
                    }
                    else
                    {
                        articleNumberExists = true;
                    }
                } while (articleNumberExists);

                // Set the article number in the product object
                product.ArticleNumber = articleNumber;

                //Set weigth t0 0 if null
                if(product.Weight == null) {
                    product.Weight = "0";
                }
              
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            //Get product Categories
            var categories = _categoryContext.ProductCategory.ToArray();
            ViewBag.Categories = categories;

            //Get product brands
            var brands = _brandContext.ProductBrand.ToArray();
            ViewBag.Brands = brands;

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ArticleNumber,AmountInStock,Title,ProductInfo,ImageName,AltText,Category,Weight,Price,Brand,Created,ImageFile")] Product product)
        {
            if (id != product.ProductId)
            {
                var message = new { message = "Fel med inläsning av produkten id"};
                return NotFound(message);
            }

            if (ModelState.IsValid)
            {

                //Check if image was uploaded
                if(product.ImageFile != null) {

                    //Save images to the image folder
                    string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extention = Path.GetExtension(product.ImageFile.FileName);

                    //Remove blank spaces
                    product.ImageName = filename = filename.Replace(" ", String.Empty) + extention;

                    string path = Path.Combine(wwwRootPath + "wwwroot" + "/" + "imageupload", filename);

                    //Store file
                    using (var fileStream = new FileStream(path, FileMode.Create)){
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                }


                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Product/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ProductContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
