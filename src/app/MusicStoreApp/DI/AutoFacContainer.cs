using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


namespace MusicStoreApp.DI
{
    public class AutoFacContainer
    {
        public static AutofacServiceProvider GetServiceProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutoFacModule());
            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
