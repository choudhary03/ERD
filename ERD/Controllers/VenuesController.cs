#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.ViewModels;
using ERD.Services;
using ERD.Service;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize]
    public class VenuesController : Controller
    {
        //private readonly ERDContext _context;
        private readonly ILogger<VenuesController> _logger;
        private readonly VenueService _venueService;
        private readonly ActivitiesService _activitiesService;

        public VenuesController(ILogger<VenuesController> logger, VenueService venueService, ActivitiesService activitiesService)
        {
            //_context = context;
            _venueService = venueService;
            _activitiesService = activitiesService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Venues Controller");
        }


        public async Task<IActionResult> Index()
        {
            return View(_venueService.ListOfVenues().ToList());
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(int id)
        {
            var obj = _venueService.GetVenueDetails(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            return View();
        }



        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venue venue)
        {
            var result = _venueService.AddVenue(venue);
            if (result == "Successfully Added")
                return RedirectToAction("Index");
            else if (result == "Venue already exists")
                ViewBag.Error = result;

            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            return View(venue);
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
            var obj = _venueService.GetVenueDetails(id);

            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            
            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            var result = _venueService.UpdateVenue(id, venue);
            if (result == "Successfully Updated")
                return RedirectToAction("Index");
            else if (result == "Venue already exists")
                ViewBag.Error = result;
            return View(venue);
            
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _venueService.GetVenueDetails(id);
            return View(obj);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _venueService.DeleteVenue(_venueService.GetVenueDetails(id));
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
