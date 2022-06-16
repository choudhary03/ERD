using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.Services;
using ERD.Models;

namespace ERD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsAPIController : ControllerBase
    {
        private readonly TeamsService teamService;
        public TeamsAPIController(TeamsService team)
        {
            teamService = team;
        }

        // GET: api/<TeamsAPIController>
        [HttpGet]
        public ActionResult<IList<Team>> GetTeams()
        {
            return teamService.AllTeam().ToList();
        }

        // GET api/<TeamsAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = teamService.GetTeamDetails(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // POST api/<TeamsAPIController>
        [HttpPost]
        public ActionResult<Team> PostTeam(Team team)
        {
            var result = teamService.CreateNewTeam(team);
            if (result == "Success")
                return Ok(team);
            else
                return BadRequest();
        }

        // PUT api/<TeamsAPIController>/5
        [HttpPut("{id}")]
        public ActionResult PutTeam(int id, Team team)
        {
            if (id != team.ID)
            {
                return BadRequest();
            }
            else
            {
                if (teamService.UpdateTeam(id, team) == "Success")
                    return Ok();
                else
                    return BadRequest();
            }
        }

        // DELETE api/<TeamsAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteTeam(int id)
        {
            var team = teamService.GetTeamDetails(id);
            if (team == null)
            {
                return NotFound();
            }
            else
            {
                teamService.DeleteTeam(team);
                return Ok();
            }
        }
    }
}
