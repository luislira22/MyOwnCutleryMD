using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MasterDataFactory
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
            // Exemplo da ligação à BD
            //var connection = "Server=lapr2019.database.windows.net,1433;User ID=lapr;Password=YoHwGciYDXaUcjmt75J6;";
            //services.AddDbContext<Context>(opt => opt.UseSqlServer(connection));
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("MDF"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.BuildServiceProvider().GetService<Context>().Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
