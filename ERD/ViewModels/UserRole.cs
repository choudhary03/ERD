using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Services;
using ERD.Models;
using Microsoft.AspNetCore.Identity;
namespace ERD.ViewModels
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public bool IsSelected { get; set; }
    }
}
