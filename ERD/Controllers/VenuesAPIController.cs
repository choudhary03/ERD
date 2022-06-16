using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ERD.Service;
using Microsoft.AspNetCore.Mvc;
using Refreshment_Dashboard.Models;


namespace ERD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesAPIController : ControllerBase
    {
        private readonly VenueService venueService;

        public VenuesAPIController(VenueService venue)
        {
            venueService = venue;
        }

        // GET: api/<VenuesAPIController>
        [HttpGet]
        public ActionResult<IList<Venue>> GetVenue()
        {
            return venueService.ListOfVenues().ToList();
        }

        // GET api/<VenuesAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Venue> GetVenue(int id)
        {
            var venue = venueService.GetVenueDetails(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }

        // POST api/<VenuesAPIController>
        [HttpPost]
        public ActionResult<Venue> PostVenue(Venue venue)
        {
            var result = venueService.AddVenue(venue);
            if (result == "Success")
                return Ok(venue);
            else
                return BadRequest();
        }

        // PUT api/<VenuesAPIController>/5
        [HttpPut("{id}")]
        public ActionResult PutVenue(int id, Venue venue)
        {
            if (id != venue.ID)
            {
                return BadRequest();
            }
            else
            {
                if (venueService.UpdateVenue(id, venue) == "Success")
                    return Ok();
                else
                    return BadRequest();
            }
        }

        // DELETE api/<VenuesAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteVenue(int id)
        {
            var venue = venueService.GetVenueDetails(id);
            if (venue == null)
            {
                return NotFound();
            }
            else
            {
                venueService.DeleteVenue(venue);
                return Ok();
            }
        }
    }
}
