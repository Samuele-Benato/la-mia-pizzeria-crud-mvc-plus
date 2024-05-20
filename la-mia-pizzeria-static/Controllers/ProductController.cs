using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }  
        
        public IActionResult Index()
        {
            return View(ProductManager.GetProducts());    
        }

        public IActionResult Details(int id)
        {
            var pizza = ProductManager.GetProduct(id);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductFormModel model = new ProductFormModel();
            model.Product = new Product();
            model.Categories = ProductManager.GetCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            ProductManager.InsertProduct(data.Product);
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var productToEdit = ProductManager.GetProduct(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                ProductFormModel model = new ProductFormModel(productToEdit, ProductManager.GetCategories());
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, ProductFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", data);
            }

            // MODIFICA TRAMITE LAMBDA
            bool result = ProductManager.UpdateProduct(id, productToEdit =>
            {
                productToEdit.Name = data.Product.Name;
                productToEdit.Description = data.Product.Description;
                productToEdit.Price = data.Product.Price;
                productToEdit.CategoryId = data.Product.CategoryId;
                //  productToEdit.Image = data.Product.Image;
            });

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (ProductManager.DeleteProduct(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
