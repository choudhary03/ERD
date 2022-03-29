using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Refreshment_Dashboard.Models
{
    public class Activity
    {
        [Key]
        [Required(ErrorMessage = "Required*")]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Required*")] 
        public string Name { get; set; }
    }
}
