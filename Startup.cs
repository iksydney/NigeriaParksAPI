using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkAPI.Folder;
using ParkAPI.ParkMapper;
using ParkAPI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkAPI
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Path")));
            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddAutoMapper(typeof(ParkMapping));
            services.AddSwaggerGen(options =>{
                options.SwaggerDoc("NationalParksAPI",
                new Microsoft.OpenApi.Models.OpenApiInfo(){
                    Title = "Parky API",
                    Version = "1",
                    Description = "National Parks in Nigeria",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ogbonnasydney@gmail.com",
                        Name = "Ogbonna Ikenna"
                        //Url = new Uri("")
                    }
                });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(commentsPath);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/NationalParksAPI/swagger.json", "NationalPark API");
                options.RoutePrefix = "";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
