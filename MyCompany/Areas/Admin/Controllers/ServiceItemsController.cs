using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
//using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceItemsController : Controller // для редактирования услуг на сайте
    {
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment; //передаём хостинг окружение. чтобы сохранять титульные картинки
        public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Edit(Guid id)
        {
            var entity = id == default ? new Domain.ServiceItem() : dataManager.ServiceItems.GetServiceItemById

(id);
            return View(entity);
        }
        [HttpPost]  //
        public IActionResult Edit(Domain.ServiceItem model, IFormFile titleImageFile) //входящий файл никак не проверяется
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                dataManager.ServiceItems.SaveServiceItem(model);  //сохраняем услугу
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());  //и перенаправляем пользователя на главную страницу в админке
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id) //можно удалять услуги из базы данных
        {
            dataManager.ServiceItems.DeleteServiceItem(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}