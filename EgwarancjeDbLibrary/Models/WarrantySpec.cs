using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public Warranty Warranty { get; set; }
    public OrderSpec OrderSpec { get; set; }

    public List<Attachment>? Attachments { get; set; }
}

