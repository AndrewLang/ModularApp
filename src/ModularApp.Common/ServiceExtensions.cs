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
using Microsoft.AspNet.Routing;

namespace ModularApp.Common
{

    public static class ServiceExtensions
    {
        public static IExtensionBuilder AddExtensions(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var assemblyProvider = serviceProvider.GetService<IAssemblyProvider>();

            var types = assemblyProvider.CandidateAssemblies
                                        .SelectMany(x => x.GetTypes().Select(t => t.GetTypeInfo()))
                                        .Where(x => typeof(IExtension).GetTypeInfo().IsAssignableFrom(x) && !x.IsAbstract);

            foreach(var type in types)
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IExtension),type.AsType()));
            }

            return new ExtensionBuilder(services);
        }
        public static IExtensionBuilder ConfigureExtensions(this IExtensionBuilder builder)
        {
            var extensions = builder.ServiceProvider.GetServices<IExtension>();

            foreach(var item in extensions)
            {
                item.ConfigureServices(builder.Services);
            }

            return builder;
        }

        public static IApplicationBuilder ConfigureExtensionRoutes(this IApplicationBuilder app,IRouteBuilder routes)
        {
            var extensions = app.ApplicationServices.GetServices<IExtension>();

            foreach(var item in extensions)
            {
                item.ConfigureRoutes(routes);
            }

            return app;
        }

        public static IApplicationBuilder UseExtensions(this IApplicationBuilder app)
        {
            var extensions = app.ApplicationServices.GetServices<IExtension>();

            foreach(var item in extensions)
            {
                item.Configure(app);
            }

            return app;
        }
    }
}
