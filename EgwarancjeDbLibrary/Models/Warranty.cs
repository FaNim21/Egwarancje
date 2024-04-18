using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public enum WarrantyStatusType
{
    Awaitng,
    Accepted,
    Declined
}
public class Warranty
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime DateOfWarranty { get; set; }

    public WarrantyStatusType Status { get; set; }

    public string? Comments { get; set; }

    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }

    public Order Order { get; set; }

    public List<WarrantySpec> WarrantySpecs { get; set; }
}

