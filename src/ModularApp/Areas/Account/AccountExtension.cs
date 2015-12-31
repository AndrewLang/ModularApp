using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Services;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;
using ModularApp.Common;

namespace Account
{
    public class AccountExtension:IExtension
    {
        public void ConfigureRoutes(IRouteBuilder routes)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IEmailSender,AuthMessageSender>();
            services.AddTransient<ISmsSender,AuthMessageSender>();

        }
    }
}
