using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksBackEnd.Domain;
using BooksBackEnd.Profiles;
using BooksBackEnd.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BooksBackEnd
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            });

            services.AddControllers();
            // AddSingleton
            //services.AddSingleton<ISystemTime, SystemTime>(); // all requests will share a single instance of this thing.
             //AddTransient - for any object I create, create a new instance of this thing.
            services.AddTransient<ISystemTime, SystemTime>();
            // AddScoped
            //services.AddScoped<ISystemTime, SystemTime>(); // share this instance for the entire request.
            services.AddDbContext<BooksDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("books")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BooksProfile());
                // they'll go here.
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mappingConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
