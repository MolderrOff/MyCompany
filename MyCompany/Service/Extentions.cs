using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Service
{
    public static class Extentions
    {
        public static string CutController(this string str)
        {
            return str.Replace("Controller", ""); //во входной строке HomeController вырезаем вхождение controller
        }
        
    }
}
