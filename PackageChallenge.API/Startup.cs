using System.Text.Json;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Package.Challenge.Application;
using Package.Challenge.Application.Interface;
using PackageChallenge.API.Filters;
using Package.Challenge.Application.Service;

namespace PackageChallenge.API
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
            services.AddControllers(setup =>
            {
                setup.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions()));

                setup.ReturnHttpNotAcceptable = true;

                setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

                setup.Filters.Add<ValidationFilter>();
            }).AddFluentValidation();
            services.AddApplication();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "PackageChallenge.API", Version = "v1"});
            });

            services.AddScoped<IFileOperationService, FileOperationService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<ISerializeData, SerializeDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PackageChallenge.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}