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
    public class AppDbContext : IdentityDbContext<IdentityUser>   // наследование от системного класса AppDbContext
       //использ технология работы с пользователями Identity
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TextField> TextFields { get; set; } //класс текстовое поле TextField мы его проецируем на базу данных
        //доменные объекты модели - создание
        public DbSet<ServiceItem> ServiceItems { get; set; }  //аналогично класс услуга

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //заполняем БД при создании сразу значениями по умолчанию
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole   //создаём роль для пользователя
            {
                Id = "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser //проверяем если сущности нет в бд, то мы создаём нового пользователя
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
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>   //промежуточная таблица системная 
            {
                RoleId = "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",  //связываем админа с его ролью
                UserId = "d0fd1403-2da6-41c3-a1df-6fe7e9d13df6"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField   //содаём три объекта в бд - текстовые поля
            {
                Id = new Guid("75923b22-ddba-412e-a03c-2d6cccde2931"),
                CodeWord = "PageIndex",  //ключевое слово (кодовое слово) PageIndex главная страница на нашем сайте
                Title = "Главная"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField   //содаём три объекта в бд - текстовые поля
            {
                Id = new Guid("31ed769f-5aa3-43c3-97ed-cda0cf820cd3"),
                CodeWord = "PageServices",  //ключевое слово наши услуги
                Title = "Наши услуги"
            });
            modelBuilder.Entity<TextField>().HasData(new TextField   //содаём три объекта в бд - текстовые поля
            {
                Id = new Guid("ade06cc3-181c-41ea-a1dd-d98e825c01a9"),
                CodeWord = "PageContacts",  //ключевое слово 
                Title = "контакты"
            });

        }

    }
}
