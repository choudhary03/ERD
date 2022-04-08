using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ERD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.Extensions.Logging;

namespace ERD.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ILogger<EnrollmentsController> _logger;
        private readonly ERDContext _context;
        public string Message { get; set; } = string.Empty;
        public EnrollmentsController(ERDContext context, ILogger<EnrollmentsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }



        // GET: Enrollments/Create
        public IActionResult Create()
        {
            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;

            //TempData["EmployeeList"] = TypeDropDown;

            return View();
        }

        // POST: Enrollments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            //if (ModelState.IsValid)
            //{
            //    var enrollmentList=_context.Enrollments.Where(x=> x.EmployeeID==enrollment.EmployeeID).ToList();
            //    foreach(var item in enrollmentList)
            //    {
            //        if(item.ActivityID==enrollment.ActivityID)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //    }

            //    var count = enrollmentList.Count();
            //    if (count < 4)
            //    {
            //        _context.Add(enrollment);
            //        _context.SaveChanges();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    else

            //        return RedirectToAction(nameof(Index));

            //}
            //return View(enrollment);

            //TempData["ExistingActivity"] = "Employee already enrolled in this activity!";
            //TempData["ExceedingActivity"] = "You cannot select more than 4 Activity";

            //var exceptionHandlerPathFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            //_logger.LogError($"The path {exceptionHandlerPathFeature.Path} " + $"threw an exception {exceptionHandlerPathFeature.Error}");
            //return View("Error");


            try
            {

                if (ModelState.IsValid)
                {
                    var count = _context.Enrollments.Where(x => x.EmployeeID == enrollment.EmployeeID).Count();


                    if (count < 4)
                    {
                        var count2 = _context.Enrollments.Where(x => x.EmployeeID == enrollment.EmployeeID && x.ActivityID == enrollment.ActivityID).Count();
                        if (count2 == 0)
                        {
                            _context.Enrollments.Add(enrollment);
                            _context.SaveChanges();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {


                            ViewBag.ExistingActivityError = "Employee already enrolled in this activity!";
                            //TempData["ExistingActivity"] = TempData["EmployeeList"];
                            //return View(enrollment);

                        }
                    }
                    else
                    {

                        ViewBag.ExceedingActivityError = "You cannot select more than 4 Activity";
                        //ViewBag.ExceedingActivity = TempData["ErrorMessage2"];

                    }
                    var TypeDropDown = _context.Employees.ToList();
                    var TypeDropDown2 = _context.Activitys.ToList();

                    ViewBag.TypeDropDown = TypeDropDown;
                    ViewBag.TypeDropDown2 = TypeDropDown2;

                }
  
            }
            catch (Exception ex)
            {
                Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                //_logger.LogInformation(ex.ToString());
            }
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                
                return RedirectToAction(nameof(Index));
            }
            //TempData["SucessMessage"] = "Enrollment" + enrollment.ID + "Saved Successfully";
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.ID == id);
        }
    }
}
