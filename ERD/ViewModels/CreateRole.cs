using System.ComponentModel.DataAnnotations;

namespace ERD.ViewModels
{
    public class CreateRole
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
