using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal //Dal:Data Access Layer, PrductDala interface üzerinden ulaşır böylece kullanılan teknoloji farketmeksizin ulaşım mümkün hale gelir
    {
        public List<ProductDetail> GetProductDetails()
        {
            using(NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetail
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName
                             };
                return result.ToList();
            }
        }
    }
}

//IProductDal, IEntityRepository'yi implement ettiğinden implementasyonlara ihtiyaç duyuyor ama biz onları EfEntityRepositoryBase kullanarak Core'dan temin ediyoruz ve buradan kullanacağımız sınıfı ve dbcontexti gönderiyoruz. Productı IEntityden miras alan Entities katmanından alıyoruz 