using DevFramework.Core.DataAccess;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //Neden direk EntityRepository kullanmadık? : Çünkü burda productlara özgü joinleme gibi işlemlerimiz olabilir. Bunlar bir imza olur bu yazdığım alana yazılırlar

        List<ProductDetail> GetProductDetails();

    }
}
