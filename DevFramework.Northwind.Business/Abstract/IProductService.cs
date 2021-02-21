using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Abstract
{
    public interface IProductService //sistemi managera bağlı kılmıyoruz böylece wcf entegre ettiğimizde sistemi wcf üzerinden de servis edebileceğiz
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product Add(Product product); 
    }
}
