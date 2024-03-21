using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class WarrantySpec
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Warranty.Id))]
    public int WarrantyId { get; set; }

    [ForeignKey(nameof(OrderSpec.Id))]
    public int OrderSpecId { get; set; }

    public string Comments { get; set; }
}

