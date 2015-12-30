using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Common
{
    public interface IExtension
    {
        void ConfigureServices(IServiceCollection services);
    }
}
