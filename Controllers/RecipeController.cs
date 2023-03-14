using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using Microsoft.Data.Sqlite;

namespace DT191G_projekt.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeContext _context;
        private readonly IWebHostEnvironment? _hostEnvironment;
        private string? wwwRootPath;

        public RecipeController(RecipeContext context, IWebHostEnvironment? hostEnvironment)
        {
            _context = context;
             wwwRootPath = _hostEnvironment?.WebRootPath;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
              return _context.Recipe != null ? 
                          View(await _context.Recipe.ToListAsync()) :
                          Problem("Entity set 'RecipeContext.Recipe'  is null.");
        }

        [HttpGet("getallrecepies")]
        public async Task<IActionResult> GetAllRecipes()
        {
            //Join tables and return as json object
            var recipes = await _context.Recipe.Include(r => r.Ingredients).ToListAsync();
            return Json(recipes);
        }


        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Join ingredient and recipe tables
            var recipe = await _context.Recipe
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Store recipe and ingredient in ViewBag
            ViewBag.RecipeTitle = recipe.Title;
            ViewBag.RecipeDescription = recipe.Description;
            ViewBag.Ingredients = recipe.Ingredients;

            return View(recipe);
        }
       

        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Ingredients,ImageFile,AltText")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                // Remove ingredients with null values - isNull on string values and has value on integer
                recipe.Ingredients = recipe.Ingredients.Where(i => !string.IsNullOrWhiteSpace(i.Name) &&
                                                                    !string.IsNullOrWhiteSpace(i.Unit) &&
                                                                    i.Quantity.HasValue).ToList();

                //Check if image was uploaded
                if(recipe.ImageFile != null) {

                    //Save images to the image folder
                    string filename = Path.GetFileNameWithoutExtension(recipe.ImageFile.FileName);
                    string extention = Path.GetExtension(recipe.ImageFile.FileName);

                    //Remove blank spaces
                    recipe.ImageName = filename = filename.Replace(" ", String.Empty) + extention;

                    //Store the absolute path in Image Name
                    var wwwroot = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                    recipe.ImageName = wwwroot + "/imageupload/" + recipe.ImageName;

                    //Store path to save image in /productimages
                    string path = Path.Combine(wwwRootPath + "wwwroot/imageupload", filename);

                    //Store file
                    using (var fileStream = new FileStream(path, FileMode.Create)){
                        await recipe.ImageFile.CopyToAsync(fileStream);
                    }

                   

                }else {
                    recipe.ImageName = null;
                }                                                    
                //Add to db and save
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Join tables where id is id
            var recipe = await _context.Recipe
                .Where(r => r.Id == id)
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }



        // POST: Recipe/Edit/5
        // Send id and recipe model as prop, and ingredient as list to be able to loop through the list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe, List<Ingredient> ingredients)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            //Check the form data
            if (ModelState.IsValid)
            {
                try
                {
                    //Update the recipe table
                    _context.Update(recipe);

                    //Look through the ingredients, find id of ingredients using the hidden id from the form, update if not null
                    foreach (var ingredient in ingredients)
                    {
                        var existingIngredient = await _context.Ingredient.FindAsync(ingredient.Id);
                        if (existingIngredient != null)
                        {
                            existingIngredient.Name = ingredient.Name;
                            existingIngredient.Quantity = ingredient.Quantity;
                            existingIngredient.Unit = ingredient.Unit;
                            //Use entity.modified to update in the db
                            _context.Entry(existingIngredient).State = EntityState.Modified;
                        }
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                //Catch errors
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View();
        }


        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            // Find ingridients
            var ingredients = await _context.Entry(recipe)
                .Collection(r => r.Ingredients)
                .Query()
                .ToListAsync();

            // Remove ingredients
            _context.Ingredient.RemoveRange(ingredients);

            // Remove recipe
            _context.Recipe.Remove(recipe);

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





                private bool RecipeExists(int id)
                {
                return (_context.Recipe?.Any(e => e.Id == id)).GetValueOrDefault();
                }
            }
        }
