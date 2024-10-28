using System.Data;

namespace Delivery.DAL.Repositories
{
    public interface IRepository<T>
    {
        void AddEntity(T entity, IDbTransaction? dbTransaction = default);
        IEnumerable<T> GetAllEntities();
    }
}
