#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;
using ERD.ViewModels;
using ERD.Services;
using Microsoft.AspNetCore.Authorization;

namespace ERD.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TeamsController : Controller
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly TeamsService _teamService;
        private readonly ActivitiesService _activitiesService;

        public TeamsController(ILogger<TeamsController> logger, TeamsService teamService, ActivitiesService activitiesService)
        {
            _teamService = teamService;
            _activitiesService = activitiesService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Teams Controller");
        }

        // GET: TeamsController
        public ActionResult Index()
        {
            return View(_teamService.AllTeam().ToList()); 
        }

        // GET: TeamsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var obj = _teamService.GetTeamDetails(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // GET: TeamsController/Create
        public IActionResult Create()
        {
            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            return View();
        }

        // POST: TeamsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Team team)
        {
            var result = _teamService.CreateNewTeam(team);
            if (result == "Successfully Created")
                return RedirectToAction("Index");
            else if (result == "Team already exists")
                ViewBag.DuplicateError = result;

            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            return View(team);
        }
        public IActionResult OverLimit()
        {
            return View();
        }

        public IActionResult Duplicate()
        {
            return View();
        }

        // GET: TeamsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var obj = _teamService.GetTeamDetails(id);

            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        // POST: TeamsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            var TypeDropDown = _activitiesService.ListOfActivities().ToList();

            ViewBag.TypeDropDown = TypeDropDown;

            var result = _teamService.UpdateTeam(id, team);
            if (result == "Successfully Updated")
                return RedirectToAction("Index");
            else if (result == "Team already exists ")
                ViewBag.DuplicateError = result;
            return View(team);
        }

        // GET: TeamsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _teamService.GetTeamDetails(id);
            return View(obj);
        }

        // POST: TeamsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _teamService.DeleteTeam(_teamService.GetTeamDetails(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
