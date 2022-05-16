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
    public class ActivitiesService
    {
        private readonly ERDContext _context;

        public ActivitiesService(ERDContext context)
        {
            _context = context;
        }



        public bool AddAnActivity(Activity activities)
        {
            try
            {
                _context.Activitys.Add(activities);
                _context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAnActivity(Activity activities)
        {
            try
            {

                var activity = _context.Activitys.Where(x => x.ID == activities.ID).FirstOrDefault();
                if (activity != null)
                {
                    _context.Activitys.Remove(activity);
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

        public bool UpdateAnActivity(Activity activities)
        {
            try
            {
                var activity = _context.Activitys.Where(x => x.ID == activities.ID).FirstOrDefault();
                if (activity != null)
                {

                    activity.Name = activities.Name;

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

        public Activity GetActivityDetails(int id)
        {
            try
            {
                var activity = _context.Activitys.Where(x => x.ID == id).FirstOrDefault();
                if (activity != null)
                {
                    return activity;
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

        public IList<Activity> ListOfActivities()
        {
            try
            {
                List<Activity> activity = _context.Activitys.ToList();
                if (activity != null)
                {
                    return activity;
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