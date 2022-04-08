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
    public class EnrollmentService
    {
        private readonly ERDContext _context;

        public EnrollmentService(ERDContext context)
        {
            _context = context;
        }



        public bool AddAnEnrollment(Enrollment enrollment)
        {
            try
            {
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAnEnrollment(Enrollment enrollment)
        {
            try
            {

                var emp = _context.Enrollments.Where(x => x.ID == enrollment.ID).FirstOrDefault();

                if (emp != null)
                {
                    _context.Enrollments.Remove(emp);
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

        public bool UpdateAnEnrollment(Enrollment enrollment)
        {
            try
            {
                var emp = _context.Enrollments.Where(x => x.ID == enrollment.ID).FirstOrDefault();
                if (emp != null)
                {
                    Enrollment ServicesEnrollment = new Enrollment();
                    ServicesEnrollment.ActivityID = enrollment.ActivityID;
                    ServicesEnrollment.EmployeeID = enrollment.EmployeeID;

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

        public Enrollment GetEnrollmentDetails(int id)
        {
            try
            {
                var enroll = _context.Enrollments.Where(x => x.ID == id).FirstOrDefault();
                if (enroll != null)
                {
                    return enroll;
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

        public IList<Enrollment> ListOfEnrollment()
        {
            try
            {
                List<Enrollment> emp = _context.Enrollments.ToList();
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