using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain
{ 
    public class TextField : EntityBase
    {
        [Required]
        public string CodeWord { get; set; } //ключевое поле , ко которому мы будем обращаться, 

        [Display(Name = "Название страницы (заголовок)")]
        public override string Title { get; set; } = "Информационная страница"; //переопределили свойство Title сделали название страницы, по умолчанию значение свойства "Информационная страница"

        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Содержание заполняется администратором";
    }
}
