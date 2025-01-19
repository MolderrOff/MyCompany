using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany.Domain;
//using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Areas.Admin.Controllers
{
    [Area("Admin")]  //атрибут ареа чтобы сработало соглашение для админ ареа
    public class TextFieldsController : Controller
    {
        private readonly DataManager dataManager;  //чтобы иметь доступ доменной модели к нашей бд
        public TextFieldsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }


        public ActionResult Edit(string codeWord)  //передаём кодовое слово, ищем в бд текстовое поле и передаём его в представление
        {
            var entity = dataManager.TextFields.GetTextFieldByCodeWord(codeWord);
            return View(entity);
        }

        // POST: TextFieldsController/Edit/5
        [HttpPost]
        public ActionResult Edit(Domain.TextField model)  //Здесь приходит модель с формы
        {
            if (ModelState.IsValid)
            {
                dataManager.TextFields.SaveTextField(model); //если модель валидная то сохраняем в бд текстовое поле 
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }         //и перенаправляем в хоумконтроллер действие индекс
           return View(model);
        }


    }
}
