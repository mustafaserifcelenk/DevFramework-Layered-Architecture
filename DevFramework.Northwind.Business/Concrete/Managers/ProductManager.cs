using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.Aspects.Postsharp;
using DevFramework.Core.DataAccess;
using System.Transactions;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.CrossCuttingConcerns.Catching.Microsoft;
using DevFramework.Core.Aspects.Postsharp.CacheAspects;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Core.Aspects.Postsharp.AuthorizationAspects;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        //private readonly IQueryableRepository<Product> _queryable;

        public ProductManager(IProductDal productDal) //, IQueryableRepository<Product> queryable
        {
            //_queryable = queryable;
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))] //Ürünle ilgili tüm cacheleri siler
        public Product Add(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product); Bunları yazmıyoruz çünkü AOP'ye uygun değil, her defasında çağırmak durumunda kalıyoruz
            return _productDal.Add(product);
            //EfProductDal efProductDal = new EfProductDal(); Böyle yaparsan businessı efye bağımlı hale getirirsin
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        //[PerformanceCounterAspect(2)] Burada özel bir intervalda istisna edebiliyoruz
        [SecuredOperation(Roles="Admin")] //Bu metodu sadece adminler çalıştırabilsin
        public List<Product> GetAll()
        {
            //_queryable.Table.Where()
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            ////Kirli yöntem
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            //        _productDal.Add(product1);
            //        //business Codes
            //        _productDal.Add(product2);
            //        scope.Complete();
            //    }
            //    catch
            //    {
            //        scope.Dispose();
            //    }
            //}

            _productDal.Add(product1);
            //business Codes
            _productDal.Add(product2);

        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Update(product);
        }
    }
}
