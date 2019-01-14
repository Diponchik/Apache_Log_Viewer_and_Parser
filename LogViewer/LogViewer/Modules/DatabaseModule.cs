using Autofac;
using LogViewer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LogViewer.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LogViewerContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.Register(CreateDbContextOptions).InstancePerLifetimeScope();
        }

        private static DbContextOptions<LogViewerContext> CreateDbContextOptions(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LogViewerContext>();
            dbContextOptionsBuilder.UseSqlServer(configuration["Data:ConnectionString"]);

            return dbContextOptionsBuilder.Options;
        }
    }
}

