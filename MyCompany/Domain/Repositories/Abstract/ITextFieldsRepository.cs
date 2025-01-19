//using MyCompany.Domain.Entities;
using MyCompany.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository      //Интерфейс
    //Для того чтобы сотрудник компании в панели администратора нажимал на кнопки и соотв
    //поведение было реализовано в приложении
    {
        IQueryable<TextField> GetTextFields(); // метод - сделать выборку всех текстовых полей
        TextField GetTextFieldById(Guid id); //выбрать текстовое поле по идентификатору
        TextField GetTextFieldByCodeWord(string codeWord);  //по её кодовому слову
        void SaveTextField(TextField entity); //сохранить соотв изменения в базу данных
        void DeleteTextField(Guid id);



    }
}
