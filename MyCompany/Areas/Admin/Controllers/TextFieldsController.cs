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
    [Area("Admin")]  
    public class TextFieldsController : Controller
    {
        private readonly DataManager dataManager;  
        public TextFieldsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public ActionResult Edit(string codeWord) 
        {
            var entity = dataManager.TextFields.GetTextFieldByCodeWord(codeWord);
            return View(entity);
        }
        // POST: TextFieldsController/Edit/5
        [HttpPost]
        public ActionResult Edit(Domain.TextField model) 
        {
            if (ModelState.IsValid)
            {
                dataManager.TextFields.SaveTextField(model); 
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }        
           return View(model);
        }
    }
}
