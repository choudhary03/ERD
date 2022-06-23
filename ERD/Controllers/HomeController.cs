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


namespace ERD.Controllers
{
    [Authorize (Roles = "SuperAdmin")]
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

        //From Second Phase
        //public IActionResult Index()
        //{
        //   var ListOfActivity = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();

        //    var GroupedListOfActivity = ListOfActivity.GroupBy(c => c.Employee.ID)
        //        .Select(d => new EnrollmentDetailViewModel
        //        {
        //           employees = ListOfActivity.Where(y => y.Employee.ID == d.Key).Select(y => y.Employee).FirstOrDefault(), 
        //           activityList = d.Select(e => e.Activity).ToList()
        //         });

        //    _logger.LogInformation("Hello, this is the index!");
        //    return View(GroupedListOfActivity);

        //From First phase
        //var ListOfActivity = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity)
        //    .GroupBy(c => c.Employee.Firstname)
        //    .Select(d => new Enrollment
        //    {
        //        EmployeeName = d.Key,
        //        ActName = string.Join(", ", d.Select(e => e.Activity.Name))
        //    });


        //return View(ListOfActivity);
       
        //}

        public IActionResult Privacy()
        {
            return View();
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}