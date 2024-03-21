using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgwarancjeDbLibrary.Models;

public class Company : User
    {
        public string NIP { get; set; }

        public string Address { get; set; }
    }

