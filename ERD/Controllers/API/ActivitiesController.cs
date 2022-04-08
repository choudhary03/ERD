using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivitiesService activityService;

        public ActivitiesController(ActivitiesService activity)
        {
            activityService = activity;
        }

        // TO get list of all activities
        [HttpGet]
        public ActionResult<IList<Activity>> GetActivity()
        {
            return activityService.ListOfActivities().ToList();
        }

        // To Get Acitivity details
        [HttpGet("{id}")]
        public ActionResult<Activity> GetActivity(int id)
        {
            var activity = activityService.GetActivityDetails(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // For updating activity details
        [HttpPut("{id}")]
        public ActionResult PutActivity(int id, Activity activity)
        {
            if (id != activity.ID)
            {
                return BadRequest();
            }

            activityService.UpdateAnActivity(activity);

            return Ok();
        }

        // For adding new activity

        [HttpPost]
        public ActionResult<Activity> PostEmployee(Activity activity)
        {
            var emp = activityService.AddAnActivity(activity);

            return Ok(emp);
        }

        // For deleting an activity
        [HttpDelete("{id}")]
        public ActionResult DeleteActivity(int id)
        {
            var employee = activityService.GetActivityDetails(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
