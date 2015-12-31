using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Builder;

namespace ModularApp.Common
{
    public interface IExtension
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureRoutes(IRouteBuilder routes);

        void Configure(IApplicationBuilder app);
    }
}
