using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParkAPI.Folder;
using ParkAPI.ParkMapper;
using ParkAPI.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;
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
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddAutoMapper(typeof(ParkMapping));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            options.GroupNameFormat = "'v'VVV" );
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            /*services.AddSwaggerGen(options =>{
                options.SwaggerDoc("ParkAPI",
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

                *//*options.SwaggerDoc("NationalParkTrails",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Parky Trails",
                    Version = "1",
                    Description = "National Park Trail in Nigeria",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "ogbonnasydney@gmail.com",
                        Name = "Ogbonna Ikenna"
                        //Url = new Uri("")
                    }
                });*//*

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                options.IncludeXmlComments(commentsPath);
            });*/
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
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
                foreach (var desc in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                options.RoutePrefix = "";
            });
            /*app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ParkAPI/swagger.json", "National Parks in Nigeria");
                //options.SwaggerEndpoint("/swagger/NationalParkTrails/swagger.json", "National Park Trail in Nigeria");
                options.RoutePrefix = "";
            });*/

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
