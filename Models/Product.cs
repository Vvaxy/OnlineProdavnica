using System.ComponentModel.DataAnnotations;

namespace OnlineProdavniceEF.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv proizvoda je obavezan.")]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cena je obavezna.")]
    [Range(1, 1000000, ErrorMessage = "Cena mora biti veća od 0.")]
    public decimal Price { get; set; }

    [Range(0, 1000, ErrorMessage = "Količina ne može biti negativna.")]
    public int Quantity { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    [StringLength(300)]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Kategorija je obavezna.")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
