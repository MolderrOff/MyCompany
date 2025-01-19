using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MyCompany.Service
{
    public class AdminAreaAuthorization : IControllerModelConvention 
    // пишем соглашение контроллеров чтобы только пользователь в роли админ мог попадать в админку
    //реализовали интерфейс IControllerModelConvention

    {
        private readonly string area;
        private readonly string policy;

        public AdminAreaAuthorization(string area, string policy)
        {
            this.area = area;
            this.policy = policy;
        }

        public void Apply(ControllerModel controller) // соглашение суть - для контроллера проверяем атрибуты
        {
            if (controller.Attributes.Any(a =>
                    a is AreaAttribute &&
                    (a as AreaAttribute).RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(r =>
                    r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) &&
                    r.Value.Equals(area, StringComparison.OrdinalIgnoreCase)))
            // если пристутствует атрибут AreaAttribut, то добавляем фильтр
            //для данного контроллера AuthorizeFilter
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }
        }


    }
}
