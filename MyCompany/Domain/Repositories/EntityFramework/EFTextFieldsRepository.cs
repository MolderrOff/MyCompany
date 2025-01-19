using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain;
//using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework //реализация имплементация нашего интерфейса для текстовых полей    
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        private readonly AppDbContext context;//Класс служт для связи объектов с бд
        public EFTextFieldsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<TextField> GetTextFields()
        {
            return context.TextFields; //обращаемся к контексту и выбираем все записи
        }

        public TextField GetTextFieldById(Guid id) //был GetTextFieldById 
        {
            return context.TextFields.FirstOrDefault(x => x.Id == id); // если хотим взять одну запись используем first to default
        }

        public TextField GetTextFieldByCodeWord(string codeWord)
        {
           return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity)  //сохраняем если идентификатор по умолчаию то ещё не создан
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; // чтобы энтити фреймворк отсдлеживал помечаем
            else
                context.Entry(entity).State = EntityState.Modified; //помечаем для контекста энтити фреймворк что модифицированная флаг Modified означает что объект есть в бд но у него изменилось значение свойств
            context.SaveChanges();
        }

        public void DeleteTextField(Guid id) //удаляем 
        {
            context.TextFields.Remove(new TextField() { Id = id}); // создаём фековый объект и делаем идентификатор в бд и по этому идентификатору будет сохранено id
            context.SaveChanges();

        }

        
    }
}
