using Dapper;
using Delivery.DAL.Entities;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace Delivery.DAL.Repositories
{
    public sealed class DeliveryOrderRepository : IDeliveryOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        public DeliveryOrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public void AddEntity(DeliveryOrderEntity entity, IDbTransaction? dbTransaction = default)
        {
            if (entity is null) return;

            dbTransaction ??= _dbConnection.BeginTransaction();

            try
            {
                var insertQuery = $@"insert into _deliveryOrder (number_order, weight_order, region_order, date_order) values(@{nameof(entity.Number)}, @{nameof(entity.Weight)}, @{nameof(entity.Region)}, @{nameof(entity.Date)})";

                _dbConnection.Execute(insertQuery, entity, dbTransaction);

                dbTransaction.Commit();
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.Message);
                dbTransaction.Rollback();
                throw;
            }
            finally { }
        }

        public IEnumerable<DeliveryOrderEntity> GetAllEntities()
        {
            try
            {
                var insertQuery = $"""
                                    select
                                    number_order as {nameof(DeliveryOrderEntity.Number)},
                                    weight_order as {nameof(DeliveryOrderEntity.Weight)},
                                    region_order as {nameof(DeliveryOrderEntity.Region)},
                                    date_order as {nameof(DeliveryOrderEntity.Date)}
                                    from _deliveryOrder
                                   """;

                var result = _dbConnection.Query<DeliveryOrderEntity>(insertQuery);

                return result;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally { }
        }
    }
}
