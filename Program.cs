using System;
using System.Collections.Generic;
using Autofac;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Reflection;

namespace Sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // Scan an assembly for components
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
            .AsImplementedInterfaces();


            var container = builder.Build();



          var solve=container.Resolve<ISolver>();
          solve.Run();
            
        }
    }
}
