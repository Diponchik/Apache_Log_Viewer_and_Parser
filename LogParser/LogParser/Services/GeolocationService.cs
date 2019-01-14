using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LogParser.Services.Interfaces;
using ILog = log4net.ILog;

namespace LogParser.Services
{
    internal class GeolocationService : IGeolocationService
    {
        private readonly ILog logger;

        public GeolocationService(ILog logger)
        {
            this.logger = logger;
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        public async Task<string> GetGeolocationByIp(string hostName)
        {
            try
            {
                var requestUri = new Uri(@"https://api.ipdata.co/" + hostName);
                var request = (HttpWebRequest) WebRequest.Create(requestUri);
                request.Proxy = null;
                request.UserAgent =
                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                using (var response = (HttpWebResponse) await request.GetResponseAsync())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var objText = await reader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<dynamic>(objText).country_name;
                    }
                }
            }
            catch (UriFormatException uriFromaException)
            {
                logger.Error($"Exeption: {uriFromaException}");
                return "N/A";
            }
            catch (WebException wex)
            {
                logger.Error($"Exeption: {wex}");
                return "N/A";
            }
            catch (Exception ex)
            {
                logger.Error($"Exeption: {ex}");
                return "N/A";
            }
        }
    }
}
