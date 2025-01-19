using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain
{
    public abstract class EntityBase //базовый класс для всех остальных сущностей, абстрактный так как смысла создавать объекты класса нет
    {
        protected EntityBase() => DateAdded = DateTime.UtcNow; //при создании каждого объекта будет защищённый конструктор дата создания будет равна 
        [Required]  //значение для этого свойства обязательное
        public Guid Id { get; set; }  // свойство ID первичный ключ

        [Display(Name = "Название (заголовок)")]    //набор свойств,  virtual чтобы в дочерних классах можно было переопределять
        public virtual string Title { get; set; }

        [Display(Name = "Краткое описание")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Полное описание")]
        public virtual string Text { get; set; }

        [Display(Name = "Титульная картинка")]
        public virtual string TitleImagePath { get; set; }  //титульная картинка TitleImagePath- путь для файла, кот будет загружаться на сервер 

        [Display(Name = "SEO  метатег Title")] //значения для метатегов
        public string MetaTitle { get; set; }

        [Display(Name = "SEO  метатег Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "SEO  метатег Keywords")]
        public string MetaKeywords { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }  // свойство - дата создания какой-то сущности
    }
}

