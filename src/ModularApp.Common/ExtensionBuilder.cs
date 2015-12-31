using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Common
{
    public class ExtensionBuilder:IExtensionBuilder
    {
        public ExtensionBuilder(IServiceCollection services)
        {
            Services = services;
            ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceCollection Services { get; }
        public IServiceProvider ServiceProvider { get; }
    }
}
