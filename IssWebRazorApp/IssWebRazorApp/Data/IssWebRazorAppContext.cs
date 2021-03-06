﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Models;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.Data
{
    public class IssWebRazorAppContext : DbContext
    {
        public IssWebRazorAppContext (DbContextOptions<IssWebRazorAppContext> options)
            : base(options)
        {
        }
        public DbSet<IssWebRazorApp.Data.PlaybookData> PlaybookData { get; set; }
        public DbSet<IssWebRazorApp.Data.CategoryData> CategoryData { get; set; }
        public DbSet<IssWebRazorApp.Data.ScheduleData> ScheduleData { get; set; }
        public DbSet<IssWebRazorApp.Data.PositionData> PositionData { get; set; }
        public DbSet<IssWebRazorApp.Data.UserData> UserData { get; set; }
        public DbSet<IssWebRazorApp.Data.FootballNoteData> FootballNoteData { get; set; }
        public DbSet<IssWebRazorApp.Data.ScheduleAnswerData> ScheduleAnswerData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ScheduleAnswerData>().HasKey(_ => new { _.ScheduleId, _.UserId });
        }
    }
}
