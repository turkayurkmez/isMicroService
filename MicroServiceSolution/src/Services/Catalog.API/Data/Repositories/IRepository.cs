using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity:class,IEntity, new()
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
