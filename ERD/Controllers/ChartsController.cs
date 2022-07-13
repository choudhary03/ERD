using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Services;
using ERD.Models;
using Microsoft.AspNetCore.Authorization;
using ERD.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace ERD.Controllers
{
    public class ChartsController : Controller
    {

        private readonly ERDContext _context;
        private readonly IMapper _mapper;

        public ChartsController(ERDContext context)
        {
            _context = context;
        }


        // GET: ChartsController
        //public JsonResult Index()
        //{
        //    var List = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
        //        return Json(List);
        //}

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Index2()
        {
            var List = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
            return Json(List);
        }

        //public ActionResult GetChart()
        //{
        //    return View();
        //}


        //public JsonResult DisplayChart()
        //{
        //    var item = _context.Enrollments.Include(x => x.Employee).Include(x => x.Activity).ToList();
        //    var mappedItem = _mapper.Map<List<EnrollmentViewModelAutomapper>>(item);

        //    var enrollmentGroupByEmployee = mappedItem.GroupBy(c => c.Firstname).Select(d => new EnrollmentViewModelAutomapper
        //     {
        //         Firstname = d.Key,
        //         ActivityList = d.Select(e => e.ActivityName).ToList(),
        //         ActivityCount = d.Select(f => f.ActivityList).Count(),
        //         ActivityCounterList = CountMod(d.Select(g => g.ActivityName).ToList())
        //     });
        //    return Json(enrollmentGroupByEmployee);

        //}
        //public List<ActivityCounter> CountMod(List<string> enrolledActivity)
        //{
        //    List<ActivityCounter> result = new List<ActivityCounter>();
        //    var listOfActivity = _context.Activitys.Select(x => x.Name).ToList();
            
        //    foreach (var row in listOfActivity)
        //    {
        //        result.Add(new ActivityCounter { Name = row, Counter = 0 });
        //    }
        //    foreach (var row in result)
        //    {
        //        foreach (var col in enrolledActivity)
        //        {
        //            if (row.Name == col)
        //            {
        //                row.Counter = 1;
        //                break;
        //            }
        //        }
        //    }
        //    return result;
        //}


    }
}
