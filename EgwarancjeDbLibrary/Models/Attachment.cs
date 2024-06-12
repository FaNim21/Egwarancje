using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EgwarancjeDbLibrary.Models;

public class Attachment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(WarrantySpec))]
    public int WarrantySpecId { get; set; }

    public string? ImagePath { get; set; }

    [JsonIgnore] public WarrantySpec? WarrantySpec { get; set; }
}

