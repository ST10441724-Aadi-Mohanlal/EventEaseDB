﻿using Microsoft.EntityFrameworkCore;

namespace EventEase.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Venue> Venue { get; set; }
   
    }
}
