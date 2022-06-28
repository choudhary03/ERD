using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Models;
using Microsoft.Extensions.Logging;
using ERD.Services;
using ERD.Service;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly BookingService _bookingService;
        private readonly ActivitiesService _activitiesService;
        private readonly VenueService _venueService;


        public BookingsController(ILogger<BookingsController> logger, ActivitiesService activityService, VenueService venueService, BookingService bookingService)
        {
            _activitiesService = activityService;
            _venueService = venueService;
            _bookingService = bookingService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Bookings Controller");
        }


        public async Task<IActionResult> Index()
        {
            return View(_bookingService.ListOfBookings().ToList());
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(int id)
        {
            var obj = _bookingService.GetBookingDetails(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var venueDropDown = _venueService.ListOfVenues().ToList();
            var activityDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.venueDropDown = venueDropDown;
            ViewBag.activityDropDown = activityDropDown;
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            var result = _bookingService.MakeBooking(booking);
            if (result == "Successfully Booked")
                return RedirectToAction("Index");
            else if (result == "Existing Booking")
                ViewBag.Error = result;

            var venueDropDown = _venueService.ListOfVenues().ToList();
            var activityDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.venueDropDown = venueDropDown;
            ViewBag.activityDropDown = activityDropDown;
            return View(booking);
        }

        public IActionResult OverLimit()
        {
            return View();
        }

        public IActionResult Duplicate()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var obj = _bookingService.GetBookingDetails(id);

            var venueDropDown = _venueService.ListOfVenues().ToList();
            var activityDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.venueDropDown = venueDropDown;
            ViewBag.activityDropDown = activityDropDown;
            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            var venueDropDown = _venueService.ListOfVenues().ToList();
            var activityDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.venueDropDown = venueDropDown;
            ViewBag.activityDropDown = activityDropDown;

            var result = _bookingService.UpdateAnBooking(id, booking);
            if (result == "Successfully Updated")
                return RedirectToAction("Index");
            else if (result == "Existing Booking")
                ViewBag.Error = result;
            return View(booking);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _bookingService.GetBookingDetails(id);
            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _bookingService.DeleteAnBooking(_bookingService.GetBookingDetails(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
