using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess
{
    public interface IQueryableRepository<T> where T:class, IEntity, new()
    {
        IQueryable<T> Table { get; }

        //Bir contexte attach olarak o context kapanmadan önce(mesela bir tolist() işleminden sonra o satırdaki sorgu kapanıyor oluyor) o contexte bağlı olaraktan gerekli operasyonlarımızı yapıyor olacağız
    }
}
