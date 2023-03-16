using hw4_InternetStore_Azure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace hw4_InternetStore_Azure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Products()
        {
            using (var db = new AppContextt())
            {
                await db.Database.EnsureCreatedAsync();
                // await db.Database.EnsureDeletedAsync();

                var products = db.Products.ToList();
                ViewBag.products = products;

                ViewBag.Categories = db.Categories.ToList();


               //var categories = db.Categories.ToList();
               //ViewBag.ProdCat = products.Join(categories, o => o.CategoryId, a => a.Id, (o, a) => new { id = o.Id, title = o.ProductName, author = $"{a.CategoryName}" }).ToList();


                //foreach (var item in data)
                //{
                //    Console.WriteLine($"Id: {item.id} - Title: {item.title} - Author: {item.author}");
                //}

                //Product prod = new Product();
                //ViewData["Category"] = db.Categories.Find(prod.CategoryId).CategoryName;

            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> DetailsProd(int id)
        {
            using (var db = new AppContextt())
            {

                Product prod = await db.Products.FindAsync(id);

                if (prod != null)
                {
                    ViewData["Categories"] =db.Categories.Find(prod.CategoryId).CategoryName;
                    return View(prod);
                }
            }
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DetailsCategory(int id)
        {
            using (var db = new AppContextt())
            {

                Category category = await db.Categories.FindAsync(id);

                if (category != null)
                {
                    ViewBag.Prod = db.Products.Where(o=>o.CategoryId==id).ToList();
                    return View(category);
                }
            }
            return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProducts(int Id, Product product)
        {
            using (var db = new AppContextt())
            {
                 product = db.Products.Where(o => o.Id == Id).FirstOrDefault();
                db.Remove(product);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Products");
        }



        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            using (var db = new AppContextt())
            {
                Category cat = db.Categories.Where(o => o.Id == Id).FirstOrDefault();
                db.Remove(cat);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> Categories()
        {
            using (var db = new AppContextt())
            {
                //await db.Database.EnsureCreatedAsync();

                var categories = db.Categories.ToList();
                ViewBag.categories = categories;
            }
            return View();
        }



        [HttpPost]
        public IActionResult AddProd([Bind("Id,ProductName,Description,Price,CategoryId")] Product product)
        {
            using (var db = new AppContextt())
            {
                if (product != null)
                {
                    db.Products.Add(product);
                    db.SaveChangesAsync();
                }

            }

            return RedirectToAction("Products");
        }

        [HttpPost]
        public IActionResult AddCategory([Bind("Id,CategoryName")] Category category)
        {
            using (var db = new AppContextt())
            {
                if (category != null)
                {
                    db.Categories.Add(category);
                    db.SaveChangesAsync();
                }

            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            using (var db = new AppContextt())
            {
               // ViewBag.categ = db.Categories.ToList();

                Category cat = db.Categories.Where(o => o.Id == id).FirstOrDefault();

                var categories = db.Categories.ToList();
                ViewBag.Categories = categories;

                return View(cat);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory2(int id, Category cat)
        {
            using (var db = new AppContextt())
            {

                cat.Id = id;
                db.Categories.Update(cat);

                await db.SaveChangesAsync();
            }


            return RedirectToAction("Categories");
        }



        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            using (var db = new AppContextt())
            {
                ViewBag.prod = db.Products.ToList();

                var categories = db.Categories.ToList();
                ViewBag.Categories = categories;

                Product product = db.Products.Where(o => o.Id == id).FirstOrDefault();


                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult>EditProduct2(int id,Product product)
        {
            using(var db = new AppContextt())
            {
               
                product.Id = id;
                db.Products.Update(product);

                await db.SaveChangesAsync();
            }


            return RedirectToAction("Products");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}