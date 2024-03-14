using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.Models
{
    public class WarrantySpec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Warranty.Id))]
        public int WarrantyId { get; set; }

        [ForeignKey(nameof(OrderSpec.Id))]
        public int OrderSpecId { get; set;}

    }
}
