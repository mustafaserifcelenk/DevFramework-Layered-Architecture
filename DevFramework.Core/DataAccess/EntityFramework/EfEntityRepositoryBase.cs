using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()

        // EntityFramework için bir base depo oluşturduk ve bu base class iki nesneye ihtiyaç duyuyor, TEntity(Veritabanı nesnesi) ve TContext(UnitOfWork Design patternlarının ef ile uygulandığı çatı) ve bu class IEntityRepositoryi implemente edicek, TEntity'yi IEntityRepository çatısı altında kullanacağız
    {
        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity); //Entry ile hangi nesneye abone olunacak yani hangi nesnenin contexti kullanılacak
                addedEntity.State = EntityState.Added; //Ef'ye nesnenin eklenecek olduğunu bildirme
                context.SaveChanges();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity); //Entry ile hangi nesneye abone olunacak yani hangi nesnenin contexti kullanılacak
                deletedEntity.State = EntityState.Deleted; //Ef'ye nesnenin silinecek olduğunu bildirme
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext()) //Yukarıdan hangi contexti gönderirsek ona göre context açacak
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                //Set diyerek ilgili TEntitye abone oluyoruz, yani onu kullanıyoruz
            }

        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity); //Entry ile hangi nesneye abone olunacak yani hangi nesnenin contexti kullanılacak
                updatedEntity.State = EntityState.Modified; //Ef'ye nesnenin güncellenecek olduğunu bildirme
                context.SaveChanges();
                return entity;
            }
        }
    }
}
