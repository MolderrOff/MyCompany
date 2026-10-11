using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using MyCompany.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain
{
    //создаём контекст базы данных
    public class AppDbContext : IdentityDbContext<IdentityUser>  
       
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; }
        
        public DbSet<ServiceItem> ServiceItems { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole  
            {
                Id = "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser 
                {
                    Id = "d0fd1403-2da6-41c3-a1df-6fe7e9d13df6",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "my@email.com",
                    NormalizedEmail = "MY@EMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                    SecurityStamp = string.Empty
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>   
            {
                RoleId = "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",  
                UserId = "d0fd1403-2da6-41c3-a1df-6fe7e9d13df6"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField   
            {
                Id = new Guid("75923b22-ddba-412e-a03c-2d6cccde2931"),
                CodeWord = "PageIndex", 
                Title = "Главная"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField  
            {
                Id = new Guid("31ed769f-5aa3-43c3-97ed-cda0cf820cd3"),
                CodeWord = "PageServices",  
                Title = "Наши услуги"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField   
            {
                Id = new Guid("ade06cc3-181c-41ea-a1dd-d98e825c01a9"),
                CodeWord = "PageContacts", 
                Title = "контакты"
            });

        }

    }
}
