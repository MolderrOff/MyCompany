//using MyCompany.Domain.Entities;
using MyCompany.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Domain.Repositories.Abstract //чтобы сотрудник компании нажимал на кнопочки и менялось
{
    public interface IServiceItemsRepository //это интерфейс, если мы хотим поменять entity, то меняем только реализацию
    {
        IQueryable<ServiceItem> GetServiceItems(); //выбрать все услуги
        ServiceItem GetServiceItemById(Guid id); //выбрать услугу по ид
        void SaveServiceItem(ServiceItem entity);  // обновить или создать услугу
        void DeleteServiceItem(Guid id);  // удалить услугу

    }
}
