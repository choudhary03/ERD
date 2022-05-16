using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Services
{
    public class EmployeeService
    {
        private readonly ERDContext _context;

        public EmployeeService(ERDContext context)
        {
            _context = context;
        }



        public bool AddAnEmployee(Employee Employee)
        {
            try
            {
                _context.Employees.Add(Employee);
                _context.SaveChanges();
                return true;

            }
            catch (DbUpdateException )
            {
                return false;
            }
        }

        public bool DeleteAnEmployee(Employee employee)
        {
            try
            {

                var emp = _context.Employees.Where(x => x.ID == employee.ID).FirstOrDefault();
                if (emp != null)
                {
                    _context.Employees.Remove(emp);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAnEmployee(Employee Employee)
        {
            try
            {
                var emp = _context.Employees.Where(x => x.ID == Employee.ID).FirstOrDefault();
                if (emp != null)
                {
                    
                    emp.Firstname = Employee.Firstname;
                    emp.Lastname = Employee.Lastname;
                    emp.Email = Employee.Email;
                    emp.Phone = Employee.Phone;

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public Employee GetEmployeeDetails(int id)
        {
            try
            {
                var emp = _context.Employees.Where(x => x.ID == id).FirstOrDefault();
                if (emp != null)
                {
                    return emp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Employee> ListOfEmployee()
        {
            try
            {
                List<Employee> emp = _context.Employees.ToList();
                if (emp != null)
                {
                    return emp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}