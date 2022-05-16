using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Refreshment_Dashboard.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }

        public DateTime BookedOn { get; set; }
        public int VenueID { get; set; }

        [ForeignKey("VenueID")]
        public virtual Venue Venue { get; set; }

        public int ActivityID { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }
        public string MatchFix { get; set; }

    }
}