using System;
using System.Collections.Generic;
using Autofac;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace LogParser
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(GetAssembliesToRegister());

            using (var container = builder.Build())
            {
                var trackService = container.Resolve<MainViewModel>();
                DataContext = trackService;
            }
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private Assembly[] GetAssembliesToRegister()
        {
            var currentAssembly = Assembly.GetExecutingAssembly().GetName();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var dependencyAssemblies = assemblies.SelectMany(assembly => assembly.GetReferencedAssemblies()).ToList();
            dependencyAssemblies.Add(currentAssembly);

            return GetAssembiles(dependencyAssemblies);
        }
        protected Assembly[] GetAssembiles(IEnumerable<AssemblyName> dependencyAssemblies)
        {
            return dependencyAssemblies
                .Distinct()
                .Select(Assembly.Load)
                .ToArray();
        }
    }
}
