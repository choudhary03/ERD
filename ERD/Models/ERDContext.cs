﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Refreshment_Dashboard.Models

{
    public class ERDContext : IdentityDbContext
    {
        public ERDContext(DbContextOptions<ERDContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>().HasIndex(x => new { x.EmployeeID, x.ActivityID }).IsUnique(true);
            modelBuilder.Entity<Employee>().HasIndex(x => new { x.Email }).IsUnique(true);
            modelBuilder.Entity<Activity>().HasIndex(x => new { x.Name }).IsUnique(true);
        }
       
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Venue> Venues { get; set; }

    }
}
