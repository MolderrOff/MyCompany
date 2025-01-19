using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain
{
    public class ServiceItem : EntityBase // второй доменный объект какая-то услуга на сайте, наследуется от базового класса
    {
        [Required(ErrorMessage = "Заполните название услуги")]
        [Display(Name = "Название услуги")]
        public override string Title { get; set; } //переопределяем свойство Title Делаем его обязательным
        // все остальные свойства возьмутся из базового класса например картинку
        [Display(Name = "Краткое содержание услуги")]
        public override string Subtitle { get; set; }

        [Display(Name = "Полное описание услуги")]
        public override string Text { get; set; }

    }
}