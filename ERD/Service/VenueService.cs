using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
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

        public bool SelectVenue(Venue venues)
        {
            try
            {
                _context.Venues.Add(venues);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAnVenue(Venue venues)
        {
            try
            {

                var emp = _context.Venues.Where(x => x.ID == venues.ID).FirstOrDefault();
                if (emp != null)
                {
                    _context.Venues.Remove(emp);
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

        public bool UpdateAnVenue(Venue venues)
        {
            try
            {
                var emp = _context.Venues.Where(x => x.ID == venues.ID).FirstOrDefault();
                if (emp != null)
                {
                    Venue ServicesVenue = new Venue();
                    ServicesVenue.ID = venues.ID;

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

        public Venue GetVenueDetails(int id)
        {
            try
            {
                var emp = _context.Venues.Where(x => x.ID == id).FirstOrDefault();
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

        public IList<Venue> ListOfVenues()
        {
            try
            {
                List<Venue> emp = _context.Venues.ToList();
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
