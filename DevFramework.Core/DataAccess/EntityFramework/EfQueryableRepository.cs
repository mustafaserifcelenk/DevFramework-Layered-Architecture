using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EfQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        //IQueryable dönüşlü implementasyon, genelde contexti kapatmadan birden fazla işlem yapıp(birkaç sorgu çekmek veya bazı business logicler sağlmak gibi) kullanmak için uygulama, OData implemantasyonları gibi bazı durumlarda lazım olabiliyor

        //Buraya sabit bir context koyamayacağımız için, context dependency injection ile implement edilecek
        private DbContext _context;

        //Burada Table bir DbSet'e karşılık geliyor, entities ise hangi T'nin verildiğini ifade ediyor
        private IDbSet<T> _entities;

        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        //Bir tabloya attach/abone olup o tablo üzerinde işlem yapmamızı sağlayacak, hangi T nesnesi verilirse o tabloya
        //public IQueryable<T> Table {
        //    get { return this.Entities; } } 
        public IQueryable<T> Table => this.Entities; //Table çağrıldığında aşağıdaki entities return edilecek demek

        //Bir üstteki tablo aslında entitiesi döndürecek, ancak şu anda entites dolu değil. Dolayısıyla bırada entitiesi çağıracak bir implementasyona ihtiyaç var. Onu da EFQueyableRepositoryi newleyen biri çağıramasın diye protected yapıyoruz, virtual ORM'lerde lazy loading için

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? ( _entities = _context.Set<T>()); }//ilk table çağrıldığında null olacağından, ilk olarak buraya düşer ve ilgili T tablosuna kayıt edilir sonra arka arkaya query için çağırılırsa aşağıdaki ilgili _entities tablosu döndürülür
        }

        //protected virtual IDbSet<T> Entities
        //{
        //    get {
        //        if (Entities == null)
        //        {
        //            _entities = _context.Set<T>(); 
        //        }
        //        return _entities;
        //    }
        //}


    }
}
