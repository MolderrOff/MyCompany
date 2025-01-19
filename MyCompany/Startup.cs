using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Domain;
using MyCompany.DAL;

namespace MyCompany
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //подключаем конфиг из appseting.json
            Configuration.Bind("Project", new Config()); //связали секцию appsetings нашего проекта с нашим классом обвёрткой config


            //подключаем нужный функционал приложения в качестве сервисов
            //чтобы asp net приложение находило что кому и куда внедрить
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>(); // если хотим сменить провайдера - urm-систему entity framework на какую - то другую, то меняем EFTextFieldsRepository на MySyste
                                // //меняем привязку конкретной реализации ITextFieldsRepository к этому интерфейсу
                                // меняем систему на трансистентную то есть в рамках одного http Запроса может быть создано сколько угодно таких объектов таких репозиториев по требованию
                                 // это рекомендуется использовать  если классы не тяжеловесные
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>(); //дата менеджер связываем с самим собой

            //подключаем контекст БД, какой именно - тот который мы создали AddDbContext
           services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString)); //создаём  databasecontext  как сервис
                                                                                               //и в качестве опции указываем что мы используем sql сервер UseSqlServer и передаём как аргумент значение для строки подключения ConnectionString
                                                                                               // которая хранится в файле appsettings json в качестве строки

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connection));  //x было options




            // настраиваем identity систему
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            // стандартный IdentityUser систему
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6; // настройки напр мин длина пароля
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //настраиваем authenification cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth"; //название куки
                options.Cookie.HttpOnly = true; //создадим аккаунт контроллер и действие в нём логин
                options.LoginPath = "/account/login";  //куда отправлять пользователя чтобы он залогинился на сайте
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            
            //настраиваем политику авторизации для Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy =>  //добавляем политику AddPolicy
                {
                    policy.RequireRole("admin");  //требуем от пользователя роль Admin
                    //контроллеры с атрибутом [area] попадают под данное соглашение
                });
            });

            //добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews(x =>        //это соглашение добавляем когда регистрируем сервис контроллеров и представлений
                {
                    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); 
                    //мы передаём для области Admin политику которая называется AdminArea (строчкой выше политика авторизации)
                })
                //выставляем совместимость с asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();






        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //!!! порядок регистрации middleware очень важен

            // в процессе разработки нам важно видеть подробную информацию об ошибках
            if (env.IsDevelopment())  //если мы находимся в окружении девелопмент, то есть в процессе создания сайта важна подробная инфа об ошибках
            {
                 app.UseDeveloperExceptionPage();  //использовать страницу исключения для разработчиков, для подробной информации
            }

            // подключаем поддержку статичных файлов в приложении (css, js  и т. д.) Из папки напр wwwroot
            app.UseStaticFiles();

            // подключаем систему маршрутизации
            app.UseRouting(); // добавляется строчка кода маршрутизации

            //подключаем аутентификацию и авторизацию
            //по документации она подключается после подключения системы маршрутизации, но до определения маршрутов
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            //регистрируем нужные нам маршруты (ендпоинты)
            app.UseEndpoints(endpoints =>   //  мы будем использовать маршруты
            {
                //объявление специального маршрута для панели администратора для области админ
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}"); //Объявим что такой сегмент в системе маршрутизации должен существовать
                //в область Admin(controllers,views) нужно пускать не всех а только админ
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); //мапимся к контроллеру, маршруту, название стандартное, дефолтный маршрут для asp net core приложения
            });
        }
    }
}
