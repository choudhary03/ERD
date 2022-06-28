#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.ViewModels;
using ERD.Services;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ERDContext _context;
        private readonly EmployeeService _employeeService;

        public EmployeesController(ERDContext context, EmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(_employeeService.ListOfEmployee().ToList());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(int id)
        {
            var employee = _employeeService.GetEmployeeDetails(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
           
            var result = _employeeService.AddAnEmployee(employee);
            if (result == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "Existing Email Address, Enter a new Email.";
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            
            var employee = _employeeService.GetEmployeeDetails(id);
            return View(employee);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            
            var result = _employeeService.UpdateAnEmployee(employee);
            if (result == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "Existing Email";
            }
            return View(employee);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var employee = _employeeService.GetEmployeeDetails(id);
            return View(employee);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var result = _employeeService.DeleteAnEmployee(_employeeService.GetEmployeeDetails(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
