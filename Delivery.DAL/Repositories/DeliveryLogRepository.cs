using Dapper;
using Delivery.DAL.Entities;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace Delivery.DAL.Repositories
{
    public sealed class DeliveryLogRepository : IDeliveryLogRepository
    {
        private readonly IDbConnection _dbConnection;

        public DeliveryLogRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void AddEntity(DeliveryLogEntity entity, IDbTransaction? dbTransaction = default)
        {
            if (entity is null) return;

            dbTransaction ??= _dbConnection.BeginTransaction();

            try
            {
                var insertQuery = $@"insert into _deliveryLog (ip_address, date, message) values(@{nameof(entity.IPAddress)}, @{nameof(entity.Date)}, @{nameof(entity.Message)})";

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

        public IEnumerable<DeliveryLogEntity> GetAllEntities()
        {
            try
            {
                var insertQuery = $"""
                                    select
                                    ip_address as {nameof(DeliveryLogEntity.IPAddress)},
                                    date as {nameof(DeliveryLogEntity.Date)},
                                    message as {nameof(DeliveryLogEntity.Message)}
                                    from _deliveryLog
                                   """;

                var result = _dbConnection.Query<DeliveryLogEntity>(insertQuery);

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
