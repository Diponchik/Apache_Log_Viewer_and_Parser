using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LogViewer.Entities;
using LogViewer.Models;
using LogViewer.Repositories.LogRepository.Interfaces;
using LogViewer.Requests;
using Microsoft.EntityFrameworkCore;

namespace LogViewer.Repositories.LogRepository
{
    public class LogReadRepository : ILogReadRepository
    {
        private readonly DbSet<LogModel> logsDbSet;

        public LogReadRepository(DbContext dbContext)
        {
            logsDbSet = dbContext.Set<LogModel>();
        }

        public Task<long> GetAllLogsCount(LogFilterRequest filter)
        {
            return logsDbSet
                .Where(CreateLogFilter(filter))
                .LongCountAsync();
        }

        public Task<List<LogModel>> GetAllLogs(LogFilterRequest filter)
        {
            return logsDbSet
                .Where(CreateLogFilter(filter))
                .OrderBy(log => log.Date)
                .Skip((filter.PageNumber - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .ToListAsync();
        }

        public async Task<IList<TopModel>> GetpTopByField(LogFilterRequest filter)
        {
            var top = filter.Top ?? 10;

            var grouppedLogs = await logsDbSet
                .Where(CreateLogFilter(filter))
                .ToListAsync();

            return grouppedLogs
                .GroupBy(CreateLogGroupping(filter))
                .Select(GetTopModel)
                .OrderByDescending(group => group.Count)
                .Take(top)
                .ToList();
        }

        private Expression<Func<LogModel, bool>> CreateLogFilter(LogFilterRequest filter)
        {
            return log => CreationFromDateFilter(filter.FromDate, log.Date)
                                && CreationToDateFilter(filter.ToDate, log.Date);
        }

        private Func<LogModel, string> CreateLogGroupping(LogFilterRequest filter)
        {
            if (filter.IsHostRequest)
                return log => log.HostName;

            return log => log.Route;
        }


        private bool CreationFromDateFilter(DateTime? selectedFromDate, DateTime creationDate)
        {
            return selectedFromDate == null || creationDate >= selectedFromDate;
        }

        private bool CreationToDateFilter(DateTime? selectedToDate, DateTime creationDate)
        {
            return selectedToDate == null || creationDate <= selectedToDate;
        }

        private TopModel GetTopModel(IGrouping<string, LogModel> group)
        {
            return new TopModel
            {
                Name = group.Key,
                Count = group.Count()
            };
        }
    }
}
