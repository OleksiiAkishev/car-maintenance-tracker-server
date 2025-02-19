﻿using CarMaintenanceTrackerServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServer.Data
{
    public class CarMaintenanceTrackerDbContext(DbContextOptions<CarMaintenanceTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
