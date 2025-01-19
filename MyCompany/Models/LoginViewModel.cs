using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; } // пользователь должен ввести на форме свой логин

        [Required]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; } //чтобы тэг хелпер понял что это не текстовое поле, а чекбокс

    }
}
