using System;
using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImpactMeasurementAPI.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                catch(Exception e)
                {
                    Console.WriteLine($"--> Could not run migrations: {e.Message}");
                }

            }

            if (!context.TrainingSessions.Any())
            {
                Console.WriteLine("--> seeding data");

                context.SaveChanges();

            }
            else
            {
                Console.WriteLine("--> we already have data");
            }
        }
    }
}