using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Refreshment_Dashboard.Models
{
    public class Enrollment
    {
        //[Key]
        //[Required(ErrorMessage = "Required*")]
        //public int ID { get; set; }

        //[Required(ErrorMessage = "Required*")]
        //[ForeignKey("ActivityID")]
        //public virtual Activity Activity { get; set; }

        //[Required(ErrorMessage = "Required*")]
        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }


        [Key]
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ActivityID { get; set; }

        //[NotMapped]
        //public string EmployeeName { get; set; }

        //[NotMapped]
        //public string ActName { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ActivityID")]
        public virtual Activity Activity { get; set; }

        public int? TeamID { get; set; }
        [ForeignKey("TeamID")]
        public virtual Team Team { get; set; }


    }
}
