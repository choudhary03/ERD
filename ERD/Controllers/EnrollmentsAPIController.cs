using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Services;

namespace ERD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentService enrollmentService;

        public EnrollmentsController(EnrollmentService enrollment)
        {
            enrollmentService = enrollment;
        }

        // GET: api/EnrollmentsAPI
        [HttpGet]
        public ActionResult<IList<Enrollment>> GetEnrollment()
        {
            return enrollmentService.ListOfEnrollment().ToList();
        }

        // GET: api/EnrollmentsAPI/5
        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollment(int id)
        {
            var enrollment = enrollmentService.GetEnrollmentDetails(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        // PUT: api/EnrollmentsAPI/5
        [HttpPut("{id}")]
        public ActionResult PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.ID)
            {
                return BadRequest();
            }
            else
            {
                enrollmentService.UpdateAnEnrollment(id, enrollment);
                return Ok();
            }
        }

        // POST: api/EnrollmentsAPI
        [HttpPost]
        public ActionResult<Enrollment> PostEnrollment(Enrollment enrollment)
        {
            var enroll = enrollmentService.AddAnEnrollment(enrollment);
            return Ok(enrollment);
        }


        // DELETE: api/EnrollmentsAPI/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEnrollment(int id)
        {
            var enrollment = enrollmentService.GetEnrollmentDetails(id)
;
            if (enrollment == null)
            {
                return NotFound();
            }
            else
            {
                enrollmentService.DeleteAnEnrollment(enrollment);
                return Ok();
            }
        }
    }
}
