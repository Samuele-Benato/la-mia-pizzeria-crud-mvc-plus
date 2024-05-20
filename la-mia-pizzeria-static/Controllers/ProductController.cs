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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            ProductManager.InsertProduct(data);
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            // Prendo il post AGGIORNATO da database, non
            // uno passato da utente alla action
            var productToEdit = ProductManager.GetProduct(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View(productToEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Product data)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", data);
            }

            // MODIFICA TRAMITE LAMBDA
            bool result = ProductManager.UpdateProduct(id, productToEdit =>
            {
                productToEdit.Name = data.Name;
                productToEdit.Description = data.Description;
                productToEdit.Price = data.Price;
              //  pizzaToEdit.Image = data.Image;
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
