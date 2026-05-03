using System.ComponentModel.DataAnnotations;

namespace OnlineProdavniceEF.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv kategorije je obavezan.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}
