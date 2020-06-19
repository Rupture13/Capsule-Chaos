using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PlayerActionValidationService.Models;
using PlayerActionValidationService.Services;

namespace PlayerActionValidationService
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Capsule-Chaos: PlayerActionValidation HTTPS API",
                    Version = "v1",
                    Description = "The PlayerActionValidation Microservice HTTPS API."
                });
            });

            //CosmosDB NoSQL
            // add this line to make sure that controllers can 
            // suppress the naming convention policy
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // register the DbContext class in DI Container
            services.AddDbContext<PerformanceValidationContext>(options =>
            {
                options.UseCosmos(Configuration["CosmosDbSettings:EndPoint"].ToString(),
                  Configuration["CosmosDbSettings:AccountKey"].ToString(),
                   Configuration["CosmosDbSettings:DatabaseName"].ToString(), opt => opt.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Gateway));
            });

            services.AddScoped<ICosmosDbService, CosmosDbService>();

            //InMemoryDatabase
            //services.AddDbContext<PerformanceValidationContext>(opt =>
            //   opt.UseInMemoryDatabase("PerformanceValidationList"));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayerActionValidationService API v1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
