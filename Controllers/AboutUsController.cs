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
    public class AboutUsController : Controller
    {
        private readonly AboutUsContext _context;
        private readonly IWebHostEnvironment? _hostEnvironment;

        private string? wwwRootPath;

        public AboutUsController(AboutUsContext context, IWebHostEnvironment? hostEnvironment)
        {
            _context = context;
            wwwRootPath = _hostEnvironment?.WebRootPath;
        }

        // GET: AboutUs
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.AboutUs != null ? 
                          View(await _context.AboutUs.ToListAsync()) :
                          Problem("Entity set 'AboutUsContext.AboutUs'  is null.");
        }

        [HttpGet("GetArticles")]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _context.AboutUs.ToListAsync();

            if (articles != null)
            {
                return Json(articles);
            }
            else
            {
                return Problem("Entity set 'AboutUsContext.AboutUs' is null.");
            }
        }

        // GET: AboutUs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                var message = new { error = "Id/Context did not exist in any article" };
                return NotFound(new JsonResult(message));
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aboutUs == null)
            {
                var message = new { error = "No found article" };
                return NotFound(new JsonResult(message));
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        [Authorize]
        [HttpPost("AboutUs/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,ImageFile,AltText")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {

                //Check if image was uploaded
                if(aboutUs.ImageFile != null) {

                    //Save images to the image folder
                    string filename = Path.GetFileNameWithoutExtension(aboutUs.ImageFile.FileName);
                    string extention = Path.GetExtension(aboutUs.ImageFile.FileName);

                    //Remove blank spaces
                    aboutUs.ImageName = filename = filename.Replace(" ", String.Empty) + extention;

                    //Store the absolute path in Image Name
                    var wwwroot = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                    aboutUs.ImageName = wwwroot + "/imageupload/" + aboutUs.ImageName;

                    //Store path to save image in /productimages
                    string path = Path.Combine(wwwRootPath + "wwwroot/imageupload", filename);

                    //Store file
                    using (var fileStream = new FileStream(path, FileMode.Create)){
                        await aboutUs.ImageFile.CopyToAsync(fileStream);
                    }

                   

                }else {
                    aboutUs.ImageName = null;
                }

                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                var message = new { error = "Id/Context did not exist in any article" };
                return NotFound(new JsonResult(message));
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                var message = new { error = "Article not found" };
                return NotFound(new JsonResult(message));
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,ImageName,ImageFile,AltText")] AboutUs aboutUs)
        {
            if (id != aboutUs.Id)
            {
                var message = new { error = "Id of article for editing did not exist" };
                return NotFound(new JsonResult(message));
            }

            if (ModelState.IsValid)
            {

                //Check if image was uploaded
                if(aboutUs.ImageFile != null) {

                    //Save images to the image folder
                    string filename = Path.GetFileNameWithoutExtension(aboutUs.ImageFile.FileName);
                    string extention = Path.GetExtension(aboutUs.ImageFile.FileName);

                    //Remove blank spaces
                    aboutUs.ImageName = filename = filename.Replace(" ", String.Empty) + extention;

                    //Store the absolute path in Image Name
                    var wwwroot = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                    aboutUs.ImageName = wwwroot + "/imageupload/" + aboutUs.ImageName;

                    //Store path to save image in /productimages
                    string path = Path.Combine(wwwRootPath + "wwwroot/imageupload", filename);

                    //Store file
                    using (var fileStream = new FileStream(path, FileMode.Create)){
                        await aboutUs.ImageFile.CopyToAsync(fileStream);
                    }

                   

                }else {
                    aboutUs.ImageName = null;
                }

                try
                {
                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.Id))
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
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                var message = new { error = "Id/Context did not exist in any article" };
                return NotFound(new JsonResult(message));
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AboutUs == null)
            {
                return Problem("Entity set 'AboutUsContext.AboutUs'  is null.");
            }
            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs != null)
            {
                _context.AboutUs.Remove(aboutUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(int id)
        {
          return (_context.AboutUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
