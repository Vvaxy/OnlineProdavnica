using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineProdavniceEF.Data;
using OnlineProdavniceEF.Models;

namespace OnlineProdavniceEF.Controllers;

public class ProductsController : Controller
{
    private bool IsLoggedIn()
    {
        return HttpContext.Session.GetString("UserEmail") != null;
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("IsAdmin") == "True";
    }

    public IActionResult Index(string? search, int? categoryId, string? sort, int page = 1)
    {
        if (!IsLoggedIn())
        {
            return RedirectToAction("Login", "Account");
        }

        AppDbContext db = new AppDbContext();

        List<Product> products = db.Products.Include(p => p.Category).ToList();//uzima proizvode iz baze i kategoriju uz svaki proizvod

        if (!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();//pretraga proizvoda po imenu
        }

        if (categoryId != null)
        {
            products = products.Where(p => p.CategoryId == categoryId).ToList();//filter po gategoriji
        }

        if (sort == "price")
        {
            products = products.OrderBy(p => p.Price).ToList();//po ceni
        }
        else if (sort == "price_desc")
        {
            products = products.OrderByDescending(p => p.Price).ToList();
        }
        else if (sort == "name_desc")
        {
            products = products.OrderByDescending(p => p.Name).ToList();//po imenu a-z
        }
        else
        {
            products = products.OrderBy(p => p.Name).ToList();//z-a
        }

        int pageSize = 3;
        int totalPages = (int)Math.Ceiling(products.Count / (double)pageSize);//3 proizvoda po strani

        List<Product> productsForPage = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();//samo proizvodi za tu strranu

        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", categoryId);
        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.Sort = sort;
        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.IsAdmin = IsAdmin();//salje sve podatke u View

        return View(productsForPage);
    }

    public IActionResult Details(int id)
    {
        if (!IsLoggedIn())
        {
            return RedirectToAction("Login", "Account");
        }

        AppDbContext db = new AppDbContext();
        Product? product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.IsAdmin = IsAdmin();
        return View(product);
    }//detalji o proizvodu

    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        AppDbContext db = new AppDbContext();
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");

        return View();
    }//kreiranje ako je admin

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            if (string.IsNullOrWhiteSpace(product.ImageUrl))
            {
                product.ImageUrl = "/images/product-placeholder.png";
            }

            AppDbContext db = new AppDbContext();
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        AppDbContext db2 = new AppDbContext();
        ViewBag.Categories = new SelectList(db2.Categories.ToList(), "Id", "Name", product.CategoryId);//lista sa kategorijama 

        return View(product);
    }

    public IActionResult Edit(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        AppDbContext db = new AppDbContext();
        Product? product = db.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            if (string.IsNullOrWhiteSpace(product.ImageUrl))
            {
                product.ImageUrl = "/images/product-placeholder.png";
            }

            AppDbContext db = new AppDbContext();
            db.Products.Update(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        AppDbContext db2 = new AppDbContext();
        ViewBag.Categories = new SelectList(db2.Categories.ToList(), "Id", "Name", product.CategoryId);

        return View(product);
    }

    public IActionResult Delete(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        AppDbContext db = new AppDbContext();
        Product? product = db.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index");
        }

        AppDbContext db = new AppDbContext();
        Product? product = db.Products.FirstOrDefault(p => p.Id == id);

        if (product != null)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}
