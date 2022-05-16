#nullable disable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ERD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.Extensions.Logging;
using ERD.Services;
using ERD.Models;


namespace ERD.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ILogger<EnrollmentsController> _logger;
        private readonly ERDContext _context;
        
        private readonly EnrollmentService _enrollmentService;
        private readonly ActivitiesService _activitiesService;
        private readonly EmployeeService _employeeService;
        private readonly TeamsService _teamService;
        public string Message { get; set; } = string.Empty;
        public EnrollmentsController(ERDContext context, ILogger<EnrollmentsController> logger, EnrollmentService enrollmentService, ActivitiesService activitiesService, EmployeeService employeeService, TeamsService teamService )
        {
            _context = context;
            _enrollmentService = enrollmentService;
            _activitiesService = activitiesService;
            _employeeService = employeeService;
            _teamService = teamService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected Enrollments Controller");
        }


        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToListAsync());
            return View(_enrollmentService.ListOfEnrollment().ToList());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var enrollment = await _context.Enrollments
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (enrollment == null)
            //{
            //    return NotFound();
            //}
            //return View(enrollment);
            var obj = _enrollmentService.GetEnrollmentDetails(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



        // GET: Enrollments/Create
        public IActionResult Create()
        {
            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;

            //TempData["EmployeeList"] = TypeDropDown;

            return View();
        }

        // POST: Enrollments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnEnrollment(Enrollment enrollment)
        {
            var result = _enrollmentService.AddAnEnrollment(enrollment);
            if (result == "Successfully Enrolled")
                return RedirectToAction("Index");
            else if (result == "Enrollment already esists")
                ViewBag.Error = result;
            else if (result == "You can choose upto 4 activities only")
                ViewBag.Error = result;
            else if (result == "Max enrollment for team reached")
                ViewBag.Error = result;

            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;

            return View(enrollment);

        }

        public IActionResult Exceeding_Enrollment()
        {
            return View();
        }

        public IActionResult Duplicate_Enrollment()
        {
            return View();
        }

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


        //try
        //{

        //if (ModelState.IsValid)
        //{
        //    var count = _context.Enrollments.Where(x => x.EmployeeID == enrollment.EmployeeID).Count();


        //    if (count < 4)
        //    {
        //        //var count2 = _context.Enrollments.Where(x => x.EmployeeID == enrollment.EmployeeID && x.ActivityID == enrollment.ActivityID).Count();
        //        //if (count2 == 0)
        //        //{
        //        try
        //        {

        //            _context.Enrollments.Add(enrollment);
        //            _context.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateException)
        //        {
        //            ViewBag.ExistingActivityError = "Employee already enrolled in this activity!";
        //            _logger.LogInformation("CREATE POST - Duplicate Activity!");
        //            //Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
        //            //_logger.LogInformation(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.ExceedingActivityError = "You cannot select more than 4 Activity";
        //        _logger.LogInformation("CREATE POST - Exceeding Activity!");
        //        //ViewBag.ExceedingActivity = TempData["ErrorMessage2"];

        //    }
        //else
        //{


        //    ViewBag.ExistingActivityError = "Employee already enrolled in this activity!";
        //    //TempData["ExistingActivity"] = TempData["EmployeeList"];
        //    //return View(enrollment);

        //}
        //}

        //var TypeDropDown = _context.Employees.ToList();
        //    var TypeDropDown2 = _context.Activitys.ToList();

        //    ViewBag.TypeDropDown = TypeDropDown;
        //    ViewBag.TypeDropDown2 = TypeDropDown2;

        //    return View(enrollment);

        //}

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var obj = _enrollmentService.GetEnrollmentDetails(id);

            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;
            return View(obj);
        }

        // POST: Enrollments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            //if (id != enrollment.ID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(enrollment);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!EnrollmentExists(enrollment.ID))
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
            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;
            var result = _enrollmentService.UpdateAnEnrollment(id, enrollment);
            if (result == "Successfully Enrolled")
                return RedirectToAction("Index");
            else if (result == "Enrollment already exists")
                ViewBag.Error = result;
            else if (result == "Max enrollment for team reached")
                ViewBag.Error = result;
            //TempData["SucessMessage"] = "Enrollment" + enrollment.ID + "Saved Successfully";
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var obj = _enrollmentService.GetEnrollmentDetails(id);
            return View(obj);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var enrollment = await _context.Enrollments
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (enrollment == null)
            //{
            //    return NotFound();
            //}

            //return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var enrollment = await _context.Enrollments.FindAsync(id);
            //_context.Enrollments.Remove(enrollment);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            var result = _enrollmentService.DeleteAnEnrollment(_enrollmentService.GetEnrollmentDetails(id));
            return RedirectToAction(nameof(Index));
        }
        private bool EnrollmentExists(int id)
            {
                return _context.Enrollments.Any(e => e.ID == id);
            }
        }
    }
