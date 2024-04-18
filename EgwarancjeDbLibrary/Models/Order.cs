using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OrderNumber { get; set; }

    public string? Comments { get; set; }

    public DateTime OrderDate { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public List<OrderSpec>? OrderSpecs { get; set; }
    public User? User { get; set; }
}

