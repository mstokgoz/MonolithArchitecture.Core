using ClassicArchitecture.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.Extensions.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyResolvers(this IServiceCollection serviceCollection, IEnumerable<ICoreModule> modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            ServiceTool.Create(serviceCollection);
        }
    }
}
