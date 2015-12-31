using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using ModularApp.Common;
using Microsoft.AspNet.Builder;

namespace ModularApp.BlogEngine
{
    public class BlogEngineExtension:Extension
    {
        public virtual void ConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(
                name: "ModularApp.BlogEngine",
                template: "{area}/test/{controller}/{action}",
                defaults: new { controller="Post", action= "Index"});
        }

    }
}
