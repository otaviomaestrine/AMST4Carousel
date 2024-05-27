using Microsoft.AspNetCore.Mvc;
using AMST4.Carousel.MVC.Context;
using AMST4_Carousel.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AMST4_Carousel.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _DataContext;
        public CategoryController(DataContext DataContext)
        {
            _DataContext = DataContext;
        }
        public IActionResult CategoryList()
        {
            var categories = _DataContext.Category.ToList();
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category, IFormFile image)
        {
            var Filename = Guid.NewGuid().ToString() + image.FileName;
            var Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Category", Filename);
            using (var stream = new FileStream(Filepath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            category.ImageUrl = Path.Combine("images", "Category", Filename);

            _DataContext.Category.Add(category);
            _DataContext.SaveChanges();
            category.Id = new Guid();
            return RedirectToAction("CategoryList");
        }
        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _DataContext.Category.Find(id);
            if (category == null)
            {
                return NotFound(); 
            }
            return View(category); 
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(Guid id)
        {
            var category = await _DataContext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _DataContext.Category.Remove(category);
            await _DataContext.SaveChangesAsync();

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category, IFormFile image)
        {
            if (image != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Category", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                category.ImageUrl = Path.Combine("images", "Category", fileName);
            }

            _DataContext.Category.Update(category);
            await _DataContext.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
    }

    }
