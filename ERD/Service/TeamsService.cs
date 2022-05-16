using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refreshment_Dashboard.Models;

namespace ERD.Services
{
    public class TeamsService
    {
        private readonly ERDContext _context;

        public TeamsService(ERDContext context)
        {
            _context = context;
        }

        public string CreateNewTeam(Team team)
        {
            try
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
                return "Successfully Created";

            }
            catch (DbUpdateException)
            {
                return "Team already exists";
            }
        }

        public bool DeleteTeam(Team team)
        {
            var obj = _context.Teams.Where(x => x.ID == team.ID).FirstOrDefault();
            if (obj != null)
            {
                _context.Teams.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public string UpdateTeam(int id, Team team)
        {
            try
            {
                var obj = GetTeamDetails(id);

                obj.Name = team.Name;
                obj.ActivityID = team.ActivityID;
                obj.MaxLimit = team.MaxLimit;

                _context.SaveChanges();
                return "Successfully Updated";
            }
            catch (DbUpdateException)
            {
                return "Team already exists ";
            }
        }

        public Team GetTeamDetails(int id)
        {
            try
            {
                var obj = _context.Teams.Where(x => x.ID == id).FirstOrDefault();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Team> AllTeam()
        {
            try
            {
                List<Team> obj = _context.Teams.Include(x => x.Activity).ToList();
                if (obj != null)
                {
                    return obj;
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}