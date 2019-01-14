using Autofac;
using DataBase.Utils.Repositories;
using DataBase.Utils.Repositories.Interfaces;

namespace DataBase.Utils.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogsWriteRepository>().As<ILogsWriteRepository>();
        }
    }
}
