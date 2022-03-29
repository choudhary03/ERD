using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Refreshment_Dashboard.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Phone { get; set; }

    }
}
