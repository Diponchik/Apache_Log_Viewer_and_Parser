using Autofac;
using LogViewer.Repositories.LogRepository;
using LogViewer.Repositories.LogRepository.Interfaces;

namespace LogViewer.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogReadRepository>().As<ILogReadRepository>();
        }
    }
}
