using ERD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ERD.Services;
using ERD.Models;
using AutoMapper;
using Newtonsoft.Json;
using System.Data;


namespace ERD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ERDContext _context;

        private readonly EnrollmentService enrollmentService;
        private readonly IMapper _mapper;

        public HomeController(ERDContext context, ILogger<HomeController> logger, EnrollmentService enrollmentServiceobject, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var objEnrollmentList = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
            var item = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
            var mappedItem = _mapper.Map<List<EnrollmentViewModelAutomapper>>(item);


            var enrollmentGroupByEmployee = item.GroupBy(c => c.Employee.Firstname)
               .Select(d => new EnrollmentViewModelAutomapper
               {
                   Firstname = d.Key,
                   ActivityList = d.Select(e => e.Activity.Name).ToList()
               });
            return View(enrollmentGroupByEmployee);

        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public ActionResult GetChart()
        {
            return View();
        }


        public JsonResult DisplayChart()
        {
            var item = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
            var mappedItem = _mapper.Map<List<EnrollmentViewModelAutomapper>>(item);

            var enrollmentGroupByEmployee = mappedItem.GroupBy(c => c.Firstname).Select(d => new EnrollmentViewModelAutomapper
            {
                Firstname = d.Key,
                ActivityList = d.Select(e => e.ActivityName).ToList(),
                ActivityCount = d.Select(f => f.ActivityList).Count(),
                ActivityCounterList = CountMod(d.Select(g => g.ActivityName).ToList())
            });
            return Json(enrollmentGroupByEmployee);

        }
        public List<ActivityCounter> CountMod(List<string> enrolledActivity)
        {
            List<ActivityCounter> result = new List<ActivityCounter>();
            var listOfActivity = _context.Activitys.Select(x => x.Name).ToList();

            foreach (var row in listOfActivity)
            {
                result.Add(new ActivityCounter { Name = row, Counter = 0 });
            }
            foreach (var row in result)
            {
                foreach (var col in enrolledActivity)
                {
                    if (row.Name == col)
                    {
                        row.Counter = 1;
                        break;
                    }
                }
            }
            return result;
        }



    }
}