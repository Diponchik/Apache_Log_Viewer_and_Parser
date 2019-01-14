using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Autofac.Extensions.DependencyInjection;

namespace LogViewer.Infrastructure.Autofac
{
    internal class AutofacRegisterer
    {
        private readonly ContainerBuilder builder;

        public AutofacRegisterer()
        {
            builder = new ContainerBuilder();
        }

        public void RegisterModules(IEnumerable<AssemblyName> assemblyNames)
        {
            var assemblies = assemblyNames
                .Distinct()
                .Select(Assembly.Load);

            builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        public void RegisterConfiguration(IConfiguration configuration)
        {
            builder.Register(context => configuration);
        }

        public IContainer Build(IServiceCollection services)
        {
            builder.Populate(services);
            return builder.Build();
        }
    }
}
