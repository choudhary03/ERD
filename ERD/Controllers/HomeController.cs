using ERD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ERDContext _context;
       


        public HomeController(ERDContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_logger.LogTrace("ILogger injected into {0}", nameof(HomeController));
        }


        public IActionResult Index()
        {
           var ListOfActivity = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();

            var GroupedListOfActivity = ListOfActivity.GroupBy(c => c.Employee.ID)
                .Select(d => new EnrollmentDetailViewModel
                {
                   employees = ListOfActivity.Where(y => y.Employee.ID == d.Key).Select(y => y.Employee).FirstOrDefault(), 
                   activityList = d.Select(e => e.Activity).ToList()
                 });
            

            return View(GroupedListOfActivity);

        //var ListOfActivity = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity)
        //    .GroupBy(c => c.Employee.Firstname)
        //    .Select(d => new Enrollment
        //    {
        //        EmployeeName = d.Key,
        //        ActName = string.Join(", ", d.Select(e => e.Activity.Name))
        //    });


        //return View(ListOfActivity);
       
        }

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