using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class OrderSpec
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Order.Id))]
    public int OrderId { get; set; }

    public string Name { get; set; }

    //realizacja to string materialow z ktorych orderSpec jest wykonany
    public string Realization { get; set; }

    public float ValueNet { get; set; }

    public float ValueGross { get; set; }

    //gwarancje chyba najlepiej bedzie podawac w dniach
    public int WarrantyLength { get; set; }

    public Order? Order { get; set; }
}

