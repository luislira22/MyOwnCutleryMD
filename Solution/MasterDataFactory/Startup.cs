using AutoMapper;
using MasterDataFactory.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddDbContext<Context>(opt => opt.UseSqlServer(Configuration.GetConnectionString("AzureDB")));
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("MasterDataFactory"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Mapper
            var mappingConfig = new MapperConfiguration(mapperConfig => mapperConfig.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors(o => o.AddPolicy("SPA", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            /*
             * services.AddCors(opt => {opt.AddPolicy("IT3Client",b =>b.WithOrigins("http://localhost:4200"));});
             */
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

            app.UseCors("SPA");
            app.UseHttpsRedirection();
            app.UseMvc();
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