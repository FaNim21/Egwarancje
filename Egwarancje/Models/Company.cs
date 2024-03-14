using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egwarancje.Models
{
    public class Company : User
    {
        public string NIP { get; set; }

        public string Address { get; set; }
    }
}
