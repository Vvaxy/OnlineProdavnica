using System.ComponentModel.DataAnnotations;

namespace OnlineProdavniceEF.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    public string Password { get; set; } = string.Empty;
}
