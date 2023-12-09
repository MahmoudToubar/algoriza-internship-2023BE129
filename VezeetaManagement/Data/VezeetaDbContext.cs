using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using VezeetaManagement.Models.Domain;

namespace VezeetaManagement.Data
{
    public class VezeetaDbContext : IdentityDbContext<ApplicationUser>
    {
        public VezeetaDbContext() 
        {
        }
        public VezeetaDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<TbSpecialization> Specializations { get; set; }
        public DbSet<TbAppointment> Appointments { get; set; }
        public DbSet<TbBooking> Booking { get; set; }
        public DbSet<TbTime> Time { get; set; }
        public DbSet<TbImage> Image { get; set; }
        public DbSet<TbDiscount> Discount { get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Appointments)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Specialization)
            .WithOne(s => s.User)
            .HasForeignKey<ApplicationUser>(u => u.SpecializationId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TbAppointment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TbAppointment>()
                .HasMany(a => a.Times)
                .WithOne(t => t.Appointments)
                .HasForeignKey(t => t.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TbAppointment>()
                .HasOne(a => a.Booking)
                .WithMany(b => b.Appointments)
                .HasForeignKey(a => a.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TbBooking>()
                .HasOne(b => b.Discount)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.DiscountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TbBooking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TbSpecialization>()
            .HasOne(s => s.User)
            .WithOne(u => u.Specialization)
            .HasForeignKey<TbSpecialization>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TbTime>()
                .HasOne(t => t.Appointments)
                .WithMany(a => a.Times)
                .HasForeignKey(t => t.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);


             // Seed Roles (User, Admin, Doctor)

            var adminRoleId = "6601fe61-af1e-4e2b-a5d9-1b6b587d9705";
            var userRoleId = "b68f3b1b-fe06-4121-aefb-416ddcfb42ea";
            var doctorRoleId = "78dbb243-704c-4710-b042-255a37821c89";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },


                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                },

                new IdentityRole
                {
                    Name = "Doctor",
                    NormalizedName = "Doctor",
                    Id = doctorRoleId,
                    ConcurrencyStamp = doctorRoleId,
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);


            // Seed Admin

            var AdminId = "a02df147-569e-4cad-b629-a44c8c11091c";
            var AdminUser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Id = AdminId
            };

            AdminUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(AdminUser, "Admin@123");


            modelBuilder.Entity<ApplicationUser>().HasData(AdminUser);



            // Add All roles to AdminUser
            var AdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = AdminId,
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = AdminId,
                },

                new IdentityUserRole<string>
                {
                    RoleId = doctorRoleId,
                    UserId = AdminId,
                }
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(AdminRoles);

        }

        

           
        




    }
}
