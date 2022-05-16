using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Refreshment_Dashboard.Models
{
    public class Team
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int MaxLimit { get; set; }
        public int ActivityID { get; set; }
        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }
    }
}
