using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    public class NhProductDal : NhEntityRepositoryBase<Product>, IProductDal
    {
        private NHibernateHelper _nHibernateHelper;
        public NhProductDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper) //base NhEntityRepositoryBase'e nhibernateHelperı yolluyor
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public List<ProductDetail> GetProductDetails()
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                var result = from p in session.Query<Product>()
                             join c in session.Query<Category>() on p.CategoryId equals c.CategoryId
                             select new ProductDetail
                             {
                                 ProductId = p.ProductId,
                                 CategoryName = c.CategoryName,
                                 ProductName = p.ProductName
                             };
                return result.ToList();
            }
        }
    }
}
