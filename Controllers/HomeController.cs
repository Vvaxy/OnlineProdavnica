using Microsoft.AspNetCore.Mvc;
using OnlineProdavniceEF.Data;

namespace OnlineProdavniceEF.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        AppDbContext db = new AppDbContext();

        ViewBag.ProductCount = db.Products.Count();
        ViewBag.CategoryCount = db.Categories.Count();

        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
