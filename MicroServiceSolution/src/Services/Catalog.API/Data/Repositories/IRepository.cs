using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity:class,IEntity, new()
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
