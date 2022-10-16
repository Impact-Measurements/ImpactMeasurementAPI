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
                
                context.MomentarilyAccelerations.AddRange(
                    new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.009732, AccelerationY = 0.001116,
                        AccelerationZ = 0.067139, Frame = 0, TrainingSessionId = 0
                    },
                    new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.012595, AccelerationY = -0.006085,
                        AccelerationZ = 0.058178, Frame = 1, TrainingSessionId = 0
                    },
                    new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.013917, AccelerationY = -0.005619,
                        AccelerationZ = 0.063357, Frame = 2, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.017616, AccelerationY = 0.005101,
                        AccelerationZ = 0.069366, Frame = 3, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.411952, AccelerationY = 0.006902,
                        AccelerationZ = 0.064761, Frame = 4, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.013877, AccelerationY = 0.010756,
                        AccelerationZ = 0.066491, Frame = 5, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -0.009217, AccelerationY = -0.005916,
                        AccelerationZ = 0.056226, Frame = 6, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = 1.373804, AccelerationY = -3.055712,
                        AccelerationZ = -5.190740, Frame = 7, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = 2.627385, AccelerationY = -1.833706,
                        AccelerationZ = -3.758188, Frame = 8, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = 4.302648, AccelerationY = 1.018646,
                        AccelerationZ = -1.958635, Frame = 9, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = 4.978043, AccelerationY = 3.547089,
                        AccelerationZ = -1.604290, Frame = 10, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = 4.182585, AccelerationY = -0.729916,
                        AccelerationZ = 0.832159, Frame = 11, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -5.476443, AccelerationY = -9.666263,
                        AccelerationZ = 7.893936, Frame = 12, TrainingSessionId = 0
                    },new MomentarilyAcceleration()
                    {
                        AccelerationX = -1.772400, AccelerationY = -0.827710,
                        AccelerationZ = -0.750875, Frame = 13, TrainingSessionId = 0
                    }
                );
                
                context.TrainingSessions.AddRange(
                    new TrainingSession() {FreeAcceleration = new List<MomentarilyAcceleration>(context.MomentarilyAccelerations.ToList()),
                        StartingTime = new DateTime(2022,10,15)}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> we already have data");
            }
        }
    }
}