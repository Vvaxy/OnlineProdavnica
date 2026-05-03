using Microsoft.AspNetCore.Mvc;
using OnlineProdavniceEF.Data;
using OnlineProdavniceEF.Models;

namespace OnlineProdavniceEF.Controllers;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        AppDbContext db = new AppDbContext();

        bool emailPostoji = db.Users.Any(u => u.Email == user.Email);

        if (emailPostoji)
        {
            ModelState.AddModelError("Email", "Korisnik sa ovim emailom već postoji.");
        }

        if (ModelState.IsValid)
        {
            user.IsAdmin = false;

            db.Users.Add(user);
            db.SaveChanges();

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

            return RedirectToAction("Index", "Home");
        }

        return View(user);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        AppDbContext db = new AppDbContext();

        if (ModelState.IsValid)
        {
            User? user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);//? znaci mzoebi ti null

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Pogrešan email ili lozinka.");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();//brise podatke o logovanom kroisniku
        return RedirectToAction("Index", "Home");
    }
}
