using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Infrastructure;
using System.Diagnostics;

namespace ModularApp.Common
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ScanExtensions(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var assemblyProvider = serviceProvider.GetService<IAssemblyProvider>();

            var types = assemblyProvider.CandidateAssemblies.SelectMany(x => x.GetTypes().Select(t => t.GetTypeInfo()))
                .Where(x => typeof(IExtension).GetTypeInfo().IsAssignableFrom(x) && !x.IsAbstract);

            //foreach(var item in types)
            //{
            //    Debug.WriteLine(item);
            //}
            //var extensionTypes = typeof(IExtension).GetTypeInfo()
            //    .Assembly
            //    .GetTypes()
            //    .Select(x => x.GetTypeInfo())
            //    .Where(x => typeof(IExtension).GetTypeInfo().IsAssignableFrom(x) && !x.IsAbstract);

            foreach(var type in types)
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IExtension),type.AsType()));
            }

            return services;
        }
        public static IServiceCollection ConfigureExtensions(this IServiceCollection services)
        {
            var extensions = services.BuildServiceProvider().GetServices<IExtension>();

            foreach(var item in extensions)
            {
                item.ConfigureServices(services);
            }

            return services;
        }
    }
}
