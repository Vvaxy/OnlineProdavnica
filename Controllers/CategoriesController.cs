using Microsoft.AspNetCore.Mvc;
using OnlineProdavniceEF.Data;
using OnlineProdavniceEF.Models;

namespace OnlineProdavniceEF.Controllers;

public class CategoriesController : Controller
{
    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("IsAdmin") == "True";
    }

    public IActionResult Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        AppDbContext db = new AppDbContext();
        List<Category> categories = db.Categories.ToList();

        return View(categories);
    }

    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Home");
        }

        if (ModelState.IsValid)
        {
            AppDbContext db = new AppDbContext();
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(category);
    }
}
