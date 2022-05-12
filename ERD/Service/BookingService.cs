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
    public class BookingService
    {
        private readonly ERDContext _context;

        public BookingService(ERDContext context)
        {
            _context = context;
        }

        public bool MakeBooking(Booking bookings)
        {
            try
            {
                _context.Bookings.Add(bookings);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAnBooking(Booking bookings)
        {
            try
            {

                var emp = _context.Bookings.Where(x => x.ID == bookings.ID).FirstOrDefault();
                if (emp != null)
                {
                    _context.Bookings.Remove(emp);
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

        public bool UpdateAnBooking(Booking bookings)
        {
            try
            {
                var emp = _context.Bookings.Where(x => x.ID == bookings.ID).FirstOrDefault();
                if (emp != null)
                {
                    Booking ServicesBooking = new Booking();
                    ServicesBooking.ID = bookings.ID;

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

        public Booking GetBookingDetails(int id)
        {
            try
            {
                var emp = _context.Bookings.Where(x => x.ID == id).FirstOrDefault();
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

        public IList<Booking> ListOfBookings()
        {
            try
            {
                List<Booking> emp = _context.Bookings.ToList();
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
