using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.Models
{
    public class OrderSpec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Order.Id))]

        public int OrderId { get; set; }

        public string Name { get; set; }
        
        //trzeba zapytac o co chodzi z ta realizacja
        public string Realization { get; set; }

        public float ValueNet { get; set; }

        public float ValueGross { get; set; }

        //gwarancje chyba najlepiej bedzie podawac w dniach
        public int WarrantyLength { get; set; }
    }
}
