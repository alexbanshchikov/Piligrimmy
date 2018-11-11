using DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSSQLRepository;
using Unity;

namespace TaxiSOS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            // Could be used to register more types
            container.RegisterType<IRepository<Clients>, MSSQLRepo<Clients>>();
            container.RegisterType<IRepository<Drivers>, MSSQLRepo<Drivers>>();
            container.RegisterType<IRepository<Orders>, MSSQLRepo<Orders>>();
            container.RegisterType<IRepository<Cards>, MSSQLRepo<Cards>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
