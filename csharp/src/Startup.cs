using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using src.Data;
using AutoMapper;
using src.Dto;
using src.Persistence.Models;
using src.Persistence.Repository;
using src.Services;

namespace src
{
    public class Startup
    {
        //public Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder AllowAnyOrigin();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlottableEntityUpdateDto, PlottableEntity>()
                    .ForMember(plottable => plottable.Latitude, opt => opt.Ignore())
                    .ForMember(plottable => plottable.Longitude, opt => opt.Ignore())
                    .ForMember(plottable => plottable.Elevation, opt => opt.Ignore())
                    .ForMember(plottable => plottable.LastUpdate, opt => opt.Ignore());

                cfg.CreateMap<MoveableEntityDto, MoveableEntity>()
                    .ForMember(moveable => moveable.Id, opt => opt.Ignore())
                    .ForMember(moveable => moveable.PlottableEntityID, opt => opt.Ignore())
                    .ForMember(moveable => moveable.PlottableEntity, opt => opt.Ignore());
                cfg.CreateMap<PlottableEntity, PlottableEntityDto>();
                cfg.CreateMap<PlottableEntity, PlottableEntityWithVectorDto>();
                cfg.CreateMap<MoveableEntity, MoveableEntityDto>();
                cfg.CreateMap<PlottableEntityWithVectorDto, PlottableEntity>()
                    .ForMember(plottable => plottable.MoveableEntity, opt => opt.Ignore());

                cfg.CreateMap<NavigationPlatform, NavigationPlatformDto>();
                cfg.CreateMap<PlottableEntity, PlottableEntityWithDistanceAndMoveableEntityDto>()
                    .ForMember(plottable => plottable.Distance, opt => opt.Ignore());

                services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                });
            });

            var mapper = configuration.CreateMapper();

            services.AddTransient<IMapper>(sp => mapper);
            services.AddTransient<IRepository<PlottableEntity>, Repository<PlottableEntity>>();
            services.AddTransient<PlottableEntityService>();
            services.AddTransient<IRepository<MoveableEntity>, Repository<MoveableEntity>>();
            services.AddTransient<MoveableEntityService>();
            services.AddTransient<IRepository<NavigationPlatform>, Repository<NavigationPlatform>>();
            services.AddTransient<NavigationPlatformService>();
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
