using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public abstract class NHibernateHelper : IDisposable //abstract yapıyoruz çünkü bunu kullancaak kişi factory initialize yapacağı için bunu ezmeli
    {
        static ISessionFactory _sessionFactory; //Factory DesignPatterndan besleniyor, her database için(oracle, mssql vs) ayrı bir konfigürasyon gönderme
        public virtual ISessionFactory SessionFactory //virtual lazy loading
        { 
            get { return _sessionFactory ?? (_sessionFactory = InitializeFactory()); } //varsa olan factoryi kullan yoksa factory initialize et, oracleın, sqlin vs
        }

        //kullanılacak veritabanı bir session factory implementasyonu yapmalı bizde onu kullanalım, bir nevi dependency injection yapacağız. Burada alt ve üstteki kodlar aynı olacakken bu ezileceği için implement edilen ortama göre değişiklik gösterecektir
        protected abstract ISessionFactory InitializeFactory();

        //Sessionu açmak, DbContexte karşılık geliyor EF'de
        public virtual ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); // Dsiposelar için standart işlem
        }
    }
}
