using System.ComponentModel.DataAnnotations;

namespace OnlineProdavniceEF.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ime je obavezno.")]
    [StringLength(50)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Email nije ispravan.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lozinka je obavezna.")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "Lozinka mora imati bar 4 karaktera.")]
    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }
}
