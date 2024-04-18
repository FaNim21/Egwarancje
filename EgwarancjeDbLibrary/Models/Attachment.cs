using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Attachment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(WarrantySpec))]
    public int WarrantySpecId { get; set; }

    public string image { get; set; }

    public WarrantySpec? WarrantySpec { get; set; }
}

