using System.Threading.Tasks;

namespace LogParser.Services.Interfaces
{
    internal interface IGeolocationService
    {
        Task<string> GetGeolocationByIp(string hostName);
    }
}
