﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAnActivity(Activity activities)
        {
            try
            {

                var emp = _context.Activitys.Where(x => x.ID == activities.ID).FirstOrDefault();
                if (emp != null)
                {
                    _context.Activitys.Remove(emp);
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

        public bool UpdateAnActivity(Activity activities)
        {
            try
            {
                var emp = _context.Activitys.Where(x => x.ID == activities.ID).FirstOrDefault();
                if (emp != null)
                {
                    Activity ServicesActivity = new Activity();
                    ServicesActivity.Name = activities.Name;

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

        public Activity GetActivityDetails(int id)
        {
            try
            {
                var emp = _context.Activitys.Where(x => x.ID == id).FirstOrDefault();
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

        public IList<Activity> ListOfActivities()
        {
            try
            {
                List<Activity> emp = _context.Activitys.ToList();
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