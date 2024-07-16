using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EgwarancjeDbLibrary.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OrderNumber { get; set; }

    public string? Comments { get; set; }

    public string? OrderDate { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public List<OrderSpec> OrderSpecs { get; set; } = [];
    [JsonIgnore] public User? User { get; set; }

    [NotMapped]
    public float GrossSum { get { return OrderSpecs.Sum(os => os.ValueGross); } private set { } }
}

