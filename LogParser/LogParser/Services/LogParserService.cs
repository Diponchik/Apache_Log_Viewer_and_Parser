using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DataBase.Utils.Entities;
using System.Globalization;
using System.Linq;
using DataBase.Utils.Repositories.Interfaces;
using LogParser.Services.Interfaces;
using ILog = log4net.ILog;

namespace LogParser.Services
{
    internal class LogParserService : ILogParserService
    {
        private const string ParsePattern = "(?<host>.*)\\s-\\s-\\s\\[(?<date>.*)\\]\\s\"(?<route>.*)\"\\s(?<status>\\d*)\\s(?<size>\\d*)";

        private readonly IGeolocationService geolocationService;
        private readonly ILogsWriteRepository logsWriteRepository;
        private readonly ILog logger;

        BlockingCollection<string> lines;
        BlockingCollection<LogModel> logs;
        readonly List<LogModel> addDbCollection = new List<LogModel>();

        public LogParserService(IGeolocationService geolocationService,
            ILogsWriteRepository logsWriteRepository,
            ILog logger)
        {
            this.geolocationService = geolocationService;
            this.logsWriteRepository = logsWriteRepository;
            this.logger = logger;
        }

        public Task ParseAsync(string pathToFile)
        {
            return ReadAndProcessFile(pathToFile);
        }

        private async Task ReadAndProcessFile(string filePath)
        {
            lines = new BlockingCollection<string>();
            logs = new BlockingCollection<LogModel>();
            addDbCollection.Clear();

            await Task.WhenAll(ReadLinesFromFile(filePath), HandleLinesFromFile(), AssignCountry());
            await logsWriteRepository.AddLogs(addDbCollection);
        }

        private Task ReadLinesFromFile(string filePath)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                            if (!CheckExeptedFiles(Regex.Match(line, ParsePattern).Groups["route"].Value))
                                lines.Add(line);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error($"Exception:{ex}");
                    throw;
                }
                finally
                {
                    lines.CompleteAdding();
                }
            });
        }

        private Task HandleLinesFromFile()
        {
            return Task.Factory.StartNew(() =>
            {
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

                Parallel.ForEach(lines.GetConsumingEnumerable(), parallelOptions, line =>
                {
                    try
                    {
                        var match = Regex.Match(line, ParsePattern);

                        if (match.Success)
                            logs.Add(GetLogModel(match));
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Exception:{ex}");
                        throw;
                    }
                });

                logs.CompleteAdding();
            });
        }

        private Task AssignCountry()
        {
            return Task.Run(() => addDbCollection.AddRange(Task.WhenAll(
                logs.GetConsumingEnumerable().AsParallel().Select(log =>
                {
                    return Task.Run(async () =>
                    {
                        var tempLog = log;
                        tempLog.Country = await geolocationService.GetGeolocationByIp(tempLog.HostName);
                        return tempLog;
                    });

                })
            ).Result)
        );
        }

        private LogModel GetLogModel(Match match)
        {
            return new LogModel
            {
                HostName = match.Groups["host"].Value,
                Date = DateTime.ParseExact(match.Groups["date"].Value, "dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture),
                Route = match.Groups["route"].Value,
                Params = GetUrlParamsIfExist(match.Groups["route"].Value),
                Status = int.Parse(match.Groups["status"].Value),
                Size = string.IsNullOrEmpty(match.Groups["size"].Value) ? 0 : long.Parse(match.Groups["size"].Value),
            };
        }

        private string GetUrlParamsIfExist(string route)
        {
            return route.Contains("?") ? route.Split('?')[1] : null;
        }

        private bool CheckExeptedFiles(string route)
        {
            return route.Contains(".gif")
                   || route.Contains(".css")
                   || route.Contains(".js");
        }
    }
}
