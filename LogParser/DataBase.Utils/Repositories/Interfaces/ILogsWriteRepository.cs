using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Utils.Entities;

namespace DataBase.Utils.Repositories.Interfaces
{
    public interface ILogsWriteRepository
    {
        Task AddLogs(IList<LogModel> logs);
    }
}
