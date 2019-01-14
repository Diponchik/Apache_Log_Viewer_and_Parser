using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using log4net;

namespace LogParser.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(CreateLogger())
                .As<ILog>()
                .SingleInstance();
        }

        private static ILog CreateLogger()
        {
            var logger = LogManager.GetLogger("Logger");
            log4net.Config.XmlConfigurator.Configure();

            return logger;
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (registration.Services.OfType<TypedService>().Any(typedService => typedService.ServiceType == typeof(ILog)))
                return;

            if (registration.Activator is ReflectionActivator reflectionActivator && !ConstructorContainsLogger(reflectionActivator))
                return;

            registration.Preparing += (sender, args) => OnPreparing(args);
        }

        private static void OnPreparing(PreparingEventArgs args)
        {
            var logger = LogManager.GetLogger("Logger");
            args.Parameters = new[] { TypedParameter.From(logger) }.Concat(args.Parameters);
        }

        private static bool ConstructorContainsLogger(ReflectionActivator reflectionActivator)
        {
            var constructors = reflectionActivator
                .ConstructorFinder
                .FindConstructors(reflectionActivator.LimitType);

            return constructors
                .SelectMany(ctor => ctor.GetParameters())
                .Any(parameterInfo => parameterInfo.ParameterType == typeof(ILog));
        }
    }
}