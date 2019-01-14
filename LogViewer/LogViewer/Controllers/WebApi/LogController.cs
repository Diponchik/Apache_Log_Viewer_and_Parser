using System.Collections.Generic;
using System.Threading.Tasks;
using LogViewer.Entities;
using LogViewer.Models;
using LogViewer.Repositories.LogRepository.Interfaces;
using LogViewer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LogViewer.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class LogController : Controller
    {
        private readonly ILogReadRepository logReadRepository;

        public LogController(ILogReadRepository logReadRepository)
        {
            this.logReadRepository = logReadRepository;
        }

        [HttpPost]
        public async Task<long> GetAllLogsCount([FromBody] LogFilterRequest filter)
        {
            return await logReadRepository.GetAllLogsCount(filter);
        }

        [HttpPost]
        public async Task<List<LogModel>> GetAllLogs([FromBody] LogFilterRequest filter)
        {
            return await logReadRepository.GetAllLogs(filter);
        }

        [HttpPost]
        public async Task<IList<TopModel>> GetTopRecords([FromBody] LogFilterRequest filter)
        {
            return await logReadRepository.GetpTopByField(filter);
        }
    }
}
