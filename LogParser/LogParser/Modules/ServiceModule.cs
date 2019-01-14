using Autofac;
using LogParser.Services;
using LogParser.Services.Interfaces;

namespace LogParser.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogParserService>().As<ILogParserService>();
            builder.RegisterType<GeolocationService>().As<IGeolocationService>();
        }
    }
}
