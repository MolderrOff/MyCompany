using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Service
{
    public class Config
    {
        public static string ConnectionString { get; set; } //каждая строчка файла конфиг соответствует каждой строчке appsetting.json
        public static string CompanyName { get; set; }
        public static string CompanyPhone { get; set; }
        public static string CompanyPhoneShort { get; set; }
        public static string CompanyEmail { get; set; } //будем вызывать значение этого свойства CompanyEmai, чтобы можно было поменять только в appsetting.json
    }

}