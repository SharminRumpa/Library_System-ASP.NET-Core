using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library_System_Project_Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Library_System_Project_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public DbSet<Library_System_Project_Core.Models.Faculty> Faculty { get; set; }

        public DbSet<Library_System_Project_Core.Models.Author> Author { get; set; }

        public DbSet<Library_System_Project_Core.Models.Department> Department { get; set; }

        public DbSet<Library_System_Project_Core.Models.IssueingBookDetails> IssueingBookDetails { get; set; }

        public DbSet<Library_System_Project_Core.Models.Publisher> Publisher { get; set; }

        public DbSet<Library_System_Project_Core.Models.Staff> Staff { get; set; }

        public DbSet<Library_System_Project_Core.Models.Student> Student { get; set; }

        public DbSet<Library_System_Project_Core.Models.Teacher> Teacher { get; set; }

        public DbSet<Library_System_Project_Core.Models.Book> Book { get; set; }

        public DbSet<Library_System_Project_Core.Models.BooksType> BooksType { get; set; }
        public DbSet<Library_System_Project_Core.Models.MenuHelperModel> MenuHelperModel { get; set; }
        public DbSet<Library_System_Project_Core.Models.MenuModel> MenuModel { get; set; }
        public DbSet<Library_System_Project_Core.Models.MenuModelManage> MenuModelManage { get; set; }

        public DbSet<Library_System_Project_Core.Models.ApplicationRole> ApplicationRole { get; set; }
        public DbSet<Library_System_Project_Core.Models.ApplicationUserRole> ApplicationUserRole { get; set; }
    }



    public class SelectedDetailsModel
    {
        public IEnumerable<Faculty> Faculty { get; set; }
        public IEnumerable<Department> Department { get; set; }
        public IEnumerable<Book> Book { get; set; }
        public IEnumerable<Author> Author { get; set; }
        public IEnumerable<Student> Student { get; set; }
        public IEnumerable<Teacher> Teacher { get; set; }
        public IEnumerable<Staff> Staff { get; set; }
        public IEnumerable<Publisher> Publisher { get; set; }
        public IEnumerable<IssueingBookDetails> IssueingBookDetails { get; set; }
        public IEnumerable<BooksType> BooksType { get; set; }
    }
}
