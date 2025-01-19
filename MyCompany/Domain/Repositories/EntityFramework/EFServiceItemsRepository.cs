using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain;
//using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;


namespace MyCompany.Domain.Repositories.EntityFramework //Для реализация репозитория для услуг на нашем сайте
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext context;

        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ServiceItem> GetServiceItems() //выбираем все записи из БД
        {
            return context.ServiceItems;
        }
        public ServiceItem GetServiceItemById(Guid id) //выбираем все записи из поля по идентификатору
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Либо админ в админке создал услугу
            else

                context.Entry(entity).State = EntityState.Modified;  //либо отредактировал существующую
            context.SaveChanges();

        }

        public void DeleteServiceItem(Guid id)
        {
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }
    }
}
