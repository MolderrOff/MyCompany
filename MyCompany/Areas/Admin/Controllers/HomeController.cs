using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")] //по этому атрибуту определяется правило Aythrization
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;//чтобы был доступ к доменным объектам

        public HomeController(DataManager dataManager) //его мы передадим через хоумконтроллер конструктор
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.ServiceItems.GetServiceItems()); //на главную страницу выведем список всех услуг которые есть на сайте
        }
        
    }
}
