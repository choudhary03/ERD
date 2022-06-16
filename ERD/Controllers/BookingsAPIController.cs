using ERD.Service;
using Microsoft.AspNetCore.Mvc;
using Refreshment_Dashboard.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsAPIController : ControllerBase
    {
        private readonly BookingService bookingService;

        public BookingsAPIController(BookingService booking)
        {
            bookingService
                = booking;
        }
        // GET: api/<BookingsAPIController>
        [HttpGet]
        public ActionResult<IList<Booking>> GetBookings()
        {
            return bookingService.ListOfBookings().ToList();
        }

        // GET api/<BookingsAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = bookingService.GetBookingDetails(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST api/<BookingsAPIController>
        [HttpPost]
        public ActionResult<Booking> PostBooking(Booking booking)
        {
            var result = bookingService.MakeBooking(booking);
            if (result == "Success")
                return Ok(booking);
            else
                return BadRequest();
        }

        // PUT api/<BookingsAPIController>/5
        [HttpPut("{id}")]
        public ActionResult PutBooking(int id, Booking booking)
        {
            if (id != booking.ID)
            {
                return BadRequest();
            }
            else
            {
                if (bookingService.UpdateAnBooking(id, booking) == "Success")
                    return Ok();
                else
                    return BadRequest();
            }
        }

        // DELETE api/<BookingsAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id)
        {
            var booking = bookingService.GetBookingDetails(id);
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                bookingService.DeleteAnBooking(booking);
                return Ok();
            }
        }
    }
}
