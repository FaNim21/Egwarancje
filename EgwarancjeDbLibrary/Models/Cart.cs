using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EgwarancjeDbLibrary.Models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [JsonIgnore] public User? User { get; set; }

    public string? Products { get; set; } = string.Empty;
}

public class CartProduct
{
    public string Name { get; set; }

    public int Amount { get; set; }
    public List<CartDetailsProduct>? Details { get; set; }
}

public class CartDetailsProduct
{
    public string Name { get; set; }

    public string Material { get; set; }
    public string Color { get; set; }
}
