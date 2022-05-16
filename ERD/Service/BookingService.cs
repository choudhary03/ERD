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
    public class BookingService
    {
        private readonly ERDContext _context;

        public BookingService(ERDContext context)
        {
            _context = context;
        }

        public string MakeBooking(Booking bookings)
        {
            try
            {
                _context.Bookings.Add(bookings);
                _context.SaveChanges();
                return "Successfully Booked";

            }
            catch (DbUpdateException)
            {
                return "Existing Booking";
            }
        }

        public bool DeleteAnBooking(Booking bookings)
        {
            try
            {

                var currentBooking = _context.Bookings.Where(x => x.ID == bookings.ID).FirstOrDefault();
                if (currentBooking != null)
                {
                    _context.Bookings.Remove(currentBooking);
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

        public string UpdateAnBooking(int id, Booking bookings)
        {
            try
            {
                //var emp = _context.Bookings.Where(x => x.ID == bookings.ID).FirstOrDefault();
                //if (emp != null)
                //{
                //    //Booking ServicesBooking = new Booking();
                //    //ServicesBooking.ID = bookings.ID;
                //    emp.BookedOn = bookings.BookedOn;
                //    emp.VenueID = bookings.VenueID;
                //    emp.ActivityID = bookings.ActivityID;

                //    _context.SaveChanges();
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                var currentBooking = GetBookingDetails(id);

                currentBooking.ID = bookings.ID;
                currentBooking.BookedOn = bookings.BookedOn;
                currentBooking.ActivityID = bookings.ActivityID;
                currentBooking.VenueID = bookings.VenueID;
                currentBooking.MatchFix = bookings.MatchFix;

                _context.SaveChanges();
                return "Success";

            }
            catch (DbUpdateException)
            {
                return "Existing Enrollment";
            }
        }

        public Booking GetBookingDetails(int id)
        {
            try
            {
                var bookingItem = _context.Bookings.Where(x => x.ID == id).FirstOrDefault();
                if (bookingItem != null)
                {
                    return bookingItem;
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
                List<Booking> bookingList = _context.Bookings.Include(x => x.Activity).Include(x => x.Venue).ToList();
                if (bookingList != null)
                {
                    return bookingList;
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
