using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeesController(EmployeeService employee)
        {
            employeeService = employee;
        }

        // To get list of all employees
        [HttpGet]
        public ActionResult<IList<Employee>> GetEmployee()
        {
            return employeeService.ListOfEmployee().ToList();
        }

        // To get employee details
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = employeeService.GetEmployeeDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // For updating Employee details
        [HttpPut("{id}")]
        public ActionResult PutEmployee(int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }

            employeeService.UpdateAnEmployee(employee);

            return Ok();
        }

        // For creating a new employee record
      
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            var emp = employeeService.AddAnEmployee(employee);

            return Ok(emp);
        }

        // For Deleting an Employee
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = employeeService.GetEmployeeDetails(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
