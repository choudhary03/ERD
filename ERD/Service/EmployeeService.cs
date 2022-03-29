using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
using Refreshment_Dashboard.Models;

namespace ERD.Services
{
    public class EmployeeMasterSL
    {
        private readonly ERDContext _context;

        public EmployeeMasterSL(ERDContext context)
        {
            _context = context;
        }



        public bool CreateEmployee(Employee Employee)
        {
            try
            {
                _context.Employees.Add(Employee);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEmployee(Employee employeeMaster)
        {
            try
            {

                var emp = _context.Employees.Where(x => x.ID == employeeMaster.ID).FirstOrDefault();
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee Employee)
        {
            try
            {
                var emp = _context.Employees.Where(x => x.ID == Employee.ID).FirstOrDefault();
                if (emp != null)
                {
                    Employee ServicesEmp = new Employee();
                    ServicesEmp.Firstname = Employee.Firstname;
                    ServicesEmp.Lastname = Employee.Lastname;
                    ServicesEmp.Email = Employee.Email;
                    ServicesEmp.Phone = Employee.Phone;

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

        public Employee GetEmployee(int id)
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

        public IList<Employee> AllEmployee()
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