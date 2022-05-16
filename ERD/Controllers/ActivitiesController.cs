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



namespace ERD.Controllers
{
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
            //return View(await _context.Activitys.ToListAsync());
            return View(_activitiesService.ListOfActivities().ToList());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var act = _activitiesService.GetActivityDetails(id);

            if (act == null)
            {
                return NotFound();
            }
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var activity = await _context.Activitys
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (activity == null)
            //{
            //    return NotFound();
            //}

            return View(act);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Activity activity)
        {
            //try
            //{

            //    if (ModelState.IsValid)
            //    {
            //        _context.Add(activity);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //catch (Exception )
            //{
            //    ViewBag.ExistingActivityError = "Activity already present.!";
            //}
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
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var activity = await _context.Activitys.FindAsync(id);
            //if (activity == null)
            //{
            //    return NotFound();
            //}
            var activity = _activitiesService.GetActivityDetails(id);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Activity activity)
        {
            //if (id != activity.ID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(activity);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ActivityExists(activity.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
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
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var activity = await _context.Activitys
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (activity == null)
            //{
            //    return NotFound();
            //}
            var activity = _activitiesService.GetActivityDetails(id);
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //var activity = await _context.Activitys.FindAsync(id);
            //_context.Activitys.Remove(activity);
            //await _context.SaveChangesAsync();
            var activity = _activitiesService.DeleteAnActivity(_activitiesService.GetActivityDetails(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool ActivityExists(int id)
        //{
        //    return _context.Activitys.Any(e => e.ID == id);
        //}
    }
}
