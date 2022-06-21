﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERD.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        //private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager /*SignInManager<IdentityUser> signInManager*/)
        {
            this.userManager = userManager;
            //this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
