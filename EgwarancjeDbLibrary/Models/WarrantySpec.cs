using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EgwarancjeDbLibrary.Models;

public class WarrantySpec
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Warranty))]
    public int WarrantyId { get; set; }

    [ForeignKey(nameof(OrderSpec))]
    public int OrderSpecId { get; set; }

    public string Comments { get; set; }

    [JsonIgnore] public Warranty Warranty { get; set; }
    public OrderSpec OrderSpec { get; set; }

    public List<Attachment>? Attachments { get; set; }
}

public record CreateWarrantySpecDto
{
    public int WarrantyId { get; set; }

    public int OrderSpecId { get; set; }

    public string Comments { get; set; }
}
