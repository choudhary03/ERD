#nullable disable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.Extensions.Logging;
using ERD.Services;
using ERD.Models;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize]
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
            return View(_enrollmentService.ListOfEnrollment().ToList());
        }

        // GET: Enrollments/Details/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Details(int id)
        {
            
            var obj = _enrollmentService.GetEnrollmentDetails(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



        // GET: Enrollments/Create
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;

            return View();
        }

        // POST: Enrollments/Create
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            var result = _enrollmentService.AddAnEnrollment(enrollment);
            if (result == "Successfully Enrolled")
                return RedirectToAction("Index");
            else if (result == "Enrollment already exists")
                ViewBag.Error = result;
            else if (result == "You can choose upto 4 activities only")
                ViewBag.Error = result;
            else if (result == "Max enrollment for team reached.")
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


        // GET: Enrollments/Edit/5
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
           
            var TypeDropDown = _context.Employees.ToList();
            var TypeDropDown2 = _context.Activitys.ToList();
            var TypeDropDown3 = _context.Teams.ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            ViewBag.TypeDropDown2 = TypeDropDown2;
            ViewBag.TypeDropDown3 = TypeDropDown3;
            var result = _enrollmentService.UpdateAnEnrollment(id, enrollment);
            if (result == "Successfully Updated")
                return RedirectToAction("Index");
            else if (result == "Enrollment already exists")
                ViewBag.Error = result;
            else if (result == "Max enrollment for team reached")
                ViewBag.Error = result;
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {

            var obj = _enrollmentService.GetEnrollmentDetails(id);
            return View(obj);
            
        }

        // POST: Enrollments/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var result = _enrollmentService.DeleteAnEnrollment(_enrollmentService.GetEnrollmentDetails(id));
            return RedirectToAction(nameof(Index));
        }
        private bool EnrollmentExists(int id)
            {
                return _context.Enrollments.Any(e => e.ID == id);
            }


        [HttpPost]
        public async Task<JsonResult> CallResult(int id)
        {
            var teamList = _teamService.AllTeam().Where(x => x.ActivityID == id).ToList();
            var teamDropDownItems = teamList.Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name }).ToList();
            var returnValue = Json(teamDropDownItems);
            return Json(teamDropDownItems);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
    }
