using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Services;
using ERD.Models;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly ERDContext _context;
        private readonly ActivitiesService _activitiesService;

        public ActivitiesController(ERDContext context, ActivitiesService activitiesService)
        {
            _context = context;
            _activitiesService = activitiesService;
        }

        // Get the list of activities
        public async Task<ActionResult<IList<Activity>>> Index()
        {
            return View(_activitiesService.ListOfActivities().ToList());
        }

        // GET: Activities/Details/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(int id)
        {
            var act = _activitiesService.GetActivityDetails(id);

            if (act == null)
            {
                return NotFound();
            }
    

            return View(act);
        }

        // GET: Activities/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Activities/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Activity activity)
        {
            
            var result = _activitiesService.AddAnActivity(activity);
            if (result == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "Activity already exists";
            }
            return View(activity);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var activity = _activitiesService.GetActivityDetails(id);
            return View(activity);
        }

        // POST: Activities/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Activity activity)
        {
            
            var result = _activitiesService.UpdateAnActivity(activity);
            if (result == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "Activity already exists";
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var activity = _activitiesService.GetActivityDetails(id);
            return View(activity);
        }

        // POST: Activities/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var activity = _activitiesService.DeleteAnActivity(_activitiesService.GetActivityDetails(id));
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
