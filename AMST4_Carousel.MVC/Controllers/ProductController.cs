using AMST4.Carousel.MVC.Context;
using AMST4_Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMST4_Carousel.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _DataContext;
        public ProductController(DataContext DataContext)
        {
            _DataContext = DataContext;
        }
        public IActionResult ProductList()
        {
            var products = _DataContext.Product.ToList();
            return View(products);
        }
        [HttpPost]
        public async Task  <IActionResult> AddProduct(Product product,IFormFile image)
        {
            var Filename = Guid.NewGuid().ToString() + image.FileName;
            var Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", Filename);
            using (var stream = new FileStream(Filepath,FileMode.Create))
            {
               await image.CopyToAsync(stream);
            }
            product.ImageUrl = Path.Combine("images", "Product", Filename);
            product.Id = Guid.NewGuid();
            _DataContext.Product.Add(product);
            _DataContext.SaveChanges();
            product.Id = new Guid();
            return RedirectToAction("ProductList");
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _DataContext.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(Guid id)
        {
            var product = await _DataContext.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _DataContext.Product.Remove(product);
            await _DataContext.SaveChangesAsync();

            return RedirectToAction("ProductList");

        }

        public async Task<IActionResult> EditProduct(Guid id, Product product, IFormFile image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }



            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            var UrlImage = Path.Combine("images", "Product", fileName);

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", product.ImageUrl);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            product.ImageUrl = UrlImage;


            _DataContext.Update(product);
            await _DataContext.SaveChangesAsync();
            return RedirectToAction(nameof(ProductList));


        }

    }


}

