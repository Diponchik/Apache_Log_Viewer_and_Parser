using System.Collections.Generic;
using System.Threading.Tasks;
using LogViewer.Entities;
using LogViewer.Models;
using LogViewer.Requests;

namespace LogViewer.Repositories.LogRepository.Interfaces
{
    public interface ILogReadRepository
    {
        Task<long> GetAllLogsCount(LogFilterRequest filter);
        Task<List<LogModel>> GetAllLogs(LogFilterRequest filter);
        Task<IList<TopModel>> GetpTopByField(LogFilterRequest filter);
    }
}
