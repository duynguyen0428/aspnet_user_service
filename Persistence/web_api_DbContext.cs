using System;
using Microsoft.EntityFrameworkCore;
using web_api.Model;
namespace web_api.Persistence
{
    public class web_api_DbContext : DbContext {
        public web_api_DbContext(DbContextOptions<web_api_DbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users {get;set;}
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // //Configure primary key
            // modelBuilder.Entity<Student>().HasKey<int>(s => s.StudentKey);
            // modelBuilder.Entity<Standard>().HasKey<int>(s => s.StandardKey);

            // //Configure composite primary key
            // modelBuilder.Entity<Student>().HasKey<int>(s => new { s.StudentKey, s.StudentName }); 

            //Configure primary key
            //modelBuilder.Entity<User>().HasKey(u => u.ID);
            //modelBuilder.Entity<UserProfile>()
            //            .HasKey(up => up.ID);
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserProfile)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .IsRequired();



        }
    }
}