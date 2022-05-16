using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Refreshment_Dashboard.Models
{
    public class Venue
    {
        [Key]
        [Required(ErrorMessage = "Required*")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Name { get; set; }
        public int ActivityID { get; set; }
        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }

        [Required(ErrorMessage = "Required*")]
        public int MaxLimit { get; set; }
    }
}
