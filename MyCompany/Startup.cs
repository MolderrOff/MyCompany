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
            //���������� ������ �� appseting.json
            Configuration.Bind("Project", new Config()); //������� ������ appsetings ������ ������� � ����� ������� �������� config


            //���������� ������ ���������� ���������� � �������� ��������
            //����� asp net ���������� �������� ��� ���� � ���� ��������
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>(); // ���� ����� ������� ���������� - urm-������� entity framework �� ����� - �� ������, �� ������ EFTextFieldsRepository �� MySyste
                                // //������ �������� ���������� ���������� ITextFieldsRepository � ����� ����������
                                // ������ ������� �� �������������� �� ���� � ������ ������ http ������� ����� ���� ������� ������� ������ ����� �������� ����� ������������ �� ����������
                                 // ��� ������������� ������������  ���� ������ �� ������������
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>(); //���� �������� ��������� � ����� �����

            //���������� �������� ��, ����� ������ - ��� ������� �� ������� AddDbContext
           services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString)); //������  databasecontext  ��� ������
                                                                                               //� � �������� ����� ��������� ��� �� ���������� sql ������ UseSqlServer � ������� ��� �������� �������� ��� ������ ����������� ConnectionString
                                                                                               // ������� �������� � ����� appsettings json � �������� ������

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connection));  //x ���� options




            // ����������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            // ����������� IdentityUser �������
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6; // ��������� ���� ��� ����� ������
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //����������� authenification cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth"; //�������� ����
                options.Cookie.HttpOnly = true; //�������� ������� ���������� � �������� � �� �����
                options.LoginPath = "/account/login";  //���� ���������� ������������ ����� �� ����������� �� �����
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            
            //����������� �������� ����������� ��� Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy =>  //��������� �������� AddPolicy
                {
                    policy.RequireRole("admin");  //������� �� ������������ ���� Admin
                    //����������� � ��������� [area] �������� ��� ������ ����������
                });
            });

            //��������� ��������� ������������ � ������������� (MVC)
            services.AddControllersWithViews(x =>        //��� ���������� ��������� ����� ������������ ������ ������������ � �������������
                {
                    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); 
                    //�� ������� ��� ������� Admin �������� ������� ���������� AdminArea (�������� ���� �������� �����������)
                })
                //���������� ������������� � asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();






        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //!!! ������� ����������� middleware ����� �����

            // � �������� ���������� ��� ����� ������ ��������� ���������� �� �������
            if (env.IsDevelopment())  //���� �� ��������� � ��������� �����������, �� ���� � �������� �������� ����� ����� ��������� ���� �� �������
            {
                 app.UseDeveloperExceptionPage();  //������������ �������� ���������� ��� �������������, ��� ��������� ����������
            }

            // ���������� ��������� ��������� ������ � ���������� (css, js  � �. �.) �� ����� ���� wwwroot
            app.UseStaticFiles();

            // ���������� ������� �������������
            app.UseRouting(); // ����������� ������� ���� �������������

            //���������� �������������� � �����������
            //�� ������������ ��� ������������ ����� ����������� ������� �������������, �� �� ����������� ���������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            //������������ ������ ��� �������� (���������)
            app.UseEndpoints(endpoints =>   //  �� ����� ������������ ��������
            {
                //���������� ������������ �������� ��� ������ �������������� ��� ������� �����
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}"); //������� ��� ����� ������� � ������� ������������� ������ ������������
                //� ������� Admin(controllers,views) ����� ������� �� ���� � ������ �����
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); //������� � �����������, ��������, �������� �����������, ��������� ������� ��� asp net core ����������
            });
        }
    }
}
