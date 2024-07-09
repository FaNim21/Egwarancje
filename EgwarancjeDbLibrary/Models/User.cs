using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public bool IsActivated { get; set; }
    
    public string Password { get; set; }

    public int PhoneNumber { get; set; }

    public List<Order>? Orders { get; set; }

    public List<Warranty>? Warranties { get; set; }
}
