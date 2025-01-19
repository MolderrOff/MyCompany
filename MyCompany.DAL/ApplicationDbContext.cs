using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MyCompany.Domain;
//using MyCompany.Domain.Entities;


// класс содержит все необходимые компоненты для работы с базой данных
// будет сущность и мы будем обращаться к этой сущности и преобразовывать эти данные
// в реальные объекты на  C# и уже с нимим будем рабтать
namespace MyCompany.DAL
{
    public class ApplicationDbContext : DbContext //конструктор нужен для того, чтобы зарегистрировать   //добавил  public
    //в нашем .net приложении сам этот класс ApplicationDbContext
    // чтобы установить связь с базой данных
    // это будет происходить в классе стартап. Наш конструктор прининимает в себя такой jeneric Обхект как DbContextOption
    // и мы передаём наш ApplicationDbContext в угловые скобки <>
    // Далее мы создаём название нашего объекта options и делаем наследование от родительского конструктора
    // а сам этот конструктор принимает на себя объект options 

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated(); // внутри нашего констуртора мы можем обращаться к такому объекту 
                                      // как Database и вызывать определённые методы , в данном случае EnsureCreated
                                      // в данном случае при запуске приложения будет создаваться БД если не создана
        }
        // для взаимодействия с нашими объектами нужно поставитьт сущность
        public DbSet<TextField> TextFields { get; set; } //Добавили первую сущность DataManager. Когда мы будем делать запрос в БД, 
                                                            // в эту сущность будут подставляться данные. Чтобы работать с этими данными м должны
                                                            // будем обратиться к этой сущности
    }
}
