using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;

namespace Refreshment_Dashboard.Models
{
    public class ContactUs
    {
        [Key]
        public int FeedbackId { get; set; }

        //[ForeignKey("EmployeeID")]
        //public virtual Employee Employee { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }


    }
}
