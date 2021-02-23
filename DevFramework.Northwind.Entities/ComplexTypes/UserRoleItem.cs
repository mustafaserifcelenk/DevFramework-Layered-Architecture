using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Entities.ComplexTypes
{
    public class UserRoleItem
    {
        //Roles ile Users tablosuna join atıp ikisini önyüz için birleştirebileceğim bir complextype'a ihtiyacım var
        public string RoleName { get; set; }//bir joinle bu bilgiye ulaşmak istiyoruz, dal'ın abstractına Iuserdal'a gidiyoruz buradadn 

    }
}
