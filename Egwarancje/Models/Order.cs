using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public string? Comments { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(User.Id))]
        public int UserId { get; set; }
    }
}
