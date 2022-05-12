using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Refreshment_Dashboard.Models
{
    public class Booking
    {
        [Key]
        [Required(ErrorMessage = "Required*")]
        public int ID { get; set; }

        [ForeignKey("VenueID")]
        public virtual Venue Venues { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }
       
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }


        public DateTime BookedOn { get; set; }
    }
}