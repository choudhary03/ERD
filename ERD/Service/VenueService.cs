using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Service
{
    public class VenueService
    {
        private readonly ERDContext _context;
        public VenueService(ERDContext context)
        {
            _context = context;
        }

        public string AddVenue(Venue venues)
        {
            try
            {
                _context.Venues.Add(venues);
                _context.SaveChanges();
                return "Successfully Added";

            }
            catch (DbUpdateException)
            {
                return "Venue already exists";
            }
        }

        public bool DeleteVenue(Venue venues)
        {
            try
            {

                var obj = _context.Venues.Where(x => x.ID == venues.ID).FirstOrDefault();
                if (obj != null)
                {
                    _context.Venues.Remove(obj);
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

        public string UpdateVenue(int id, Venue venues)
        {
            try
            {
                //var emp = _context.Venues.Where(x => x.ID == venues.ID).FirstOrDefault();
                //if (emp != null)
                //{
                //    //Venue ServicesVenue = new Venue();
                //    //ServicesVenue.ID = venues.ID;

                //    emp.Name = venues.Name;
                //    emp.ActivityID = venues.ActivityID;

                //    _context.SaveChanges();
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                var obj = GetVenueDetails(id);

                obj.Name = venues.Name;
                obj.ActivityID = venues.ActivityID;

                _context.SaveChanges();
                return "Successfully Updated";
            }
            catch (DbUpdateException)
            {
                return "Venue already exists";
            }
        }

        public Venue GetVenueDetails(int id)
        {
            try
            {
                var obj = _context.Venues.Where(x => x.ID == id).FirstOrDefault();
                if (obj != null)
                {
                    return obj;
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

        public IList<Venue> ListOfVenues()
        {
            try
            {
                List<Venue> obj = _context.Venues.Include(x => x.Activity).ToList();
                if (obj != null)
                {
                    return obj;
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
