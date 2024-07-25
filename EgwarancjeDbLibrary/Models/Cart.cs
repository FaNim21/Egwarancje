using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User? User { get; set; }

    public string? Product { get; set; } 
}

public class CartProduct
{
    public string Name { get; set; }

    public int Amount { get; set; }
    public string Materials { get; set; }
    public string Color { get; set; }
}
