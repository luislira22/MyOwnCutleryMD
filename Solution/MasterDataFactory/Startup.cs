using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasterDataFactory.DTO;
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
            /*var connection = "Server=lapr2019.database.windows.net;Port=5432;User Id=lapr;Password=YoHwGciYDXaUcjmt75J6;";
            services.AddDbContext<Context>(opt => opt.UseSqlServer(connection));*/
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("Machine"));
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("ProductionLine"));
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("MachineType"));
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("Operation"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //Mapper
            var mappingConfig = new MapperConfiguration(mapperConfig => mapperConfig.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
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
            //UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<Context>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}