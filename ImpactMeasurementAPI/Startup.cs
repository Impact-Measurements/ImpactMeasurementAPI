using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ImpactMeasurementAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
            
            services.AddControllers();
            services.AddScoped<IFreeAccelerationRepo, FreeAccelerationRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ImpactMeasurementAPI", Version = "v1"});
            });

            if (_env.IsProduction())
            {
                var connectionString = Configuration["mysqlconnection:connectionString2"];

                services.AddDbContext<AppDbContext>(opt =>
                    // opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));
                    opt.UseMySQL(connectionString));
            }
            else
            {
                Console.WriteLine("--> Using InMemory Db");
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseInMemoryDatabase("InMemory"));
            }

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
            
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImpactMeasurementAPI v1"));
            }

            app.UseHttpsRedirection();
            

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            PrepDb.PrepPopulation(app, env.IsProduction());
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}