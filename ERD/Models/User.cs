using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Refreshment_Dashboard.Models
{
    public class User
    {
        [Key]
        
        public string Username { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required*")]
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

    }
}
