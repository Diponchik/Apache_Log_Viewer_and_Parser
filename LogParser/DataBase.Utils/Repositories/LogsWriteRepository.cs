using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Utils.Entities;
using DataBase.Utils.Repositories.Interfaces;
using DataBase.Utils.Services;

namespace DataBase.Utils.Repositories
{
    public class LogsWriteRepository : ILogsWriteRepository
    {
        public async Task AddLogs(IList<LogModel> logs)
        {
            await Task.Factory.StartNew(() =>
            {
                var objBulk = new BulkUploadToSqlService<LogModel>
                {
                    InternalStore = logs,
                    TableName = "LogModels",
                    CommitBatchSize = 1000,
                    ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\LogDatabase.mdf;InitialCatalog=LogDatabase;Integrated Security=True;MultipleActiveResultSets=True"
                };
                objBulk.Commit();
            });
        }
    }
}
