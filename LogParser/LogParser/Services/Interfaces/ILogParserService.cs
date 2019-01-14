using System.Threading.Tasks;

namespace LogParser.Services.Interfaces
{
    internal interface ILogParserService
    {
        Task ParseAsync(string pathToFile);
    }
}
