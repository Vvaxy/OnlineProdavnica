using Microsoft.AspNetCore.Mvc;
using OnlineProdavniceEF.Data;
using OnlineProdavniceEF.Models;

namespace OnlineProdavniceEF.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        string? email = HttpContext.Session.GetString("UserEmail");

        if (email == null)
        {
            return RedirectToAction("Login", "Account");
        }

        AppDbContext db = new AppDbContext();
        User? user = db.Users.FirstOrDefault(u => u.Email == email);

        return View(user);
    }
}
