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
    public class EnrollmentService
    {
        private readonly ERDContext _context;

        public EnrollmentService(ERDContext context)
        {
            _context = context;
        }

        public string AddAnEnrollment(Enrollment enrollment)
        {
            var count = _context.Enrollments.Where(x => x.EmployeeID == enrollment.EmployeeID).Count();
            var teamMaxCount = _context.Enrollments.Where(x => x.TeamID == enrollment.TeamID).Where(x => x.ActivityID == enrollment.ActivityID).Count();

            var currentTeam = _context.Teams.Where(x => x.ID == enrollment.TeamID).Where(x => x.ActivityID == enrollment.ActivityID).FirstOrDefault();
            if (teamMaxCount < currentTeam.MaxLimit)
            {
                if (count < 4)
                {
                    try
                    {
                        _context.Enrollments.Add(enrollment);
                        _context.SaveChanges();
                        return "Successfully Enrolled";

                    }
                    catch (DbUpdateException)
                    {
                        return "Enrollment already exists";
                    }
                }
                else
                    return "You can choose upto 4 activities only";
            }
            else
            {
                return "Max enrollment for team reached.";
            }

        }

        public bool DeleteAnEnrollment(Enrollment enrollment)
        {
            try
            {

                var enroll = _context.Enrollments.Where(x => x.ID == enrollment.ID).FirstOrDefault();

                if (enroll != null)
                {
                    _context.Enrollments.Remove(enroll);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception )
            {
                return false;
            }
        }

        public string UpdateAnEnrollment(int ID, Enrollment enrollment)
        {
           
            try
            {
                var teamMaxCount = _context.Enrollments.Where(x => x.TeamID == enrollment.TeamID).Where(x => x.ActivityID == enrollment.ActivityID).Count();

                var currentTeam = _context.Teams.Where(x => x.ID == enrollment.TeamID).Where(x => x.ActivityID == enrollment.ActivityID).FirstOrDefault();

                if (teamMaxCount < currentTeam.MaxLimit)
                {
                    var enroll = GetEnrollmentDetails(ID);

                    enroll.EmployeeID = enrollment.EmployeeID;
                    enroll.ActivityID = enrollment.ActivityID;
                    enroll.TeamID = enrollment.TeamID;

                    _context.SaveChanges();
                    return "Successfully Updated";
                }
                else
                    return "Max enrollment for team reached";
            }
            catch (DbUpdateException)
            {
                return "Enrollment already exists";
            }

        }

        public Enrollment GetEnrollmentDetails(int id)
        {
            try
            {
                var enroll = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).Include(x => x.Team).Where(x => x.ID == id).FirstOrDefault();
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
                List<Enrollment> enroll = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).Include(x => x.Team).ToList();
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
    }
}