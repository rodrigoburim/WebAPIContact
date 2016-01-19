using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIContact.Models
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        void Create(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity entity);
        void Delete(int id);
        void Dispose();
    }
}
