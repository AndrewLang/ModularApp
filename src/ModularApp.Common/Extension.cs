using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Common
{
    public abstract class Extension:IExtension
    {
        public virtual void Configure(IApplicationBuilder app)
        {
            
        }

        public virtual void ConfigureRoutes(IRouteBuilder routes)
        {
            
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
