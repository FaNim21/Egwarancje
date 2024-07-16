using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Country { get; set; } // Kraj

    public string Province { get; set; } // Województwo

    public string City { get; set; } // Miasto

    public string ZipCode { get; set; } // Kod pocztowy

    public string Street { get; set; } // Ulica

    public string Number { get; set; } // Numer (dom/mieszkanie)

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User User { get; set; }
}
