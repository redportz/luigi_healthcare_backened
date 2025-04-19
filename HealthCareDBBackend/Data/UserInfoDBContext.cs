using HealthcareDBBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthcareDBBackend.Data
{
    public class UserInfoDBContext : DbContext
    {
        public UserInfoDBContext(DbContextOptions<UserInfoDBContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<PatientDetails> Patients { get; set; }
        public DbSet<DoctorDetails> Doctors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define foreign key relationship
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.User)
                .WithMany(u => u.Prescriptions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed default roles
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo { UserId = 1, FirstName = "Admin", LastName = "User", DateOfBirth = DateTime.Now, SSN = "000000000", Email = "admin@example.com", Password = "pass123", Role = "Admin" }
            );
        }
    }
}
