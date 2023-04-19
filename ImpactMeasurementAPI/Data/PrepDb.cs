using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImpactMeasurementAPI.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
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
                catch (Exception e)
                {
                    Console.WriteLine($"--> Could not run migrations: {e.Message}");
                }
            }

            // if (!context.TrainingSessions.Any())
            // {
            //     Console.WriteLine("--> seeding data");
            //     
            //     // context.Users.AddRange(
            //     //     new Athlete(){Mass = 78, Name = "Yorgo", Id = 2}, new Athlete(){Mass = 80, Name = "Jenson"});
            //     //
            //     // context.TrainingSessions.AddRange(new TrainingSession(){UserId = 1, PainfulnessScore = 7, EffectivenessScore = 6, StartingTime = new DateTime(2022, 12,01)});
            //     //
            //     // context.MomentarilyAccelerations.AddRange(
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.012595,AccelerationY = -0.006085,AccelerationZ = 0.058178,Frame = 1,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.009732,AccelerationY = 0.001116,AccelerationZ = 0.067139,Frame = 2,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -1.458654,AccelerationY = 3.102588,AccelerationZ = -0.472939,Frame = 3,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.626857,AccelerationY = -0.687684,AccelerationZ = 0.435602,Frame = 4,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.683582,AccelerationY = -0.391024,AccelerationZ = 0.844103,Frame = 5,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.559066,AccelerationY = 4.031957,AccelerationZ = 10.256809,Frame = 6,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 1.816366,AccelerationY = -6.141838,AccelerationZ = -1.298570,Frame = 7,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.382893,AccelerationY = -4.495519,AccelerationZ = -3.364853,Frame = 8,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -1.749627,AccelerationY = -3.034500,AccelerationZ = -5.596578,Frame = 9,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 0.014953,AccelerationY = -2.469700,AccelerationZ = -6.118611,Frame = 10,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 2.931997,AccelerationY = -3.741066,AccelerationZ = -5.726499,Frame = 11,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 2.539665,AccelerationY = 0.624355,AccelerationZ = -3.625031,Frame = 12,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -4.278415,AccelerationY = 5.195113,AccelerationZ = 0.476713,Frame = 13,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -4.463142,AccelerationY = -0.799354,AccelerationZ = 0.869729,Frame = 14,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -1.107420,AccelerationY = -2.967907,AccelerationZ = 0.136728,Frame = 15,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 1.280606,AccelerationY = -3.324308,AccelerationZ = -2.869083,Frame = 16,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 1.497885,AccelerationY = -2.578404,AccelerationZ = -3.762148,Frame = 17,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 0.312483,AccelerationY = 1.019125,AccelerationZ = -2.959429,Frame = 18,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -1.480753,AccelerationY = 3.548680,AccelerationZ = -0.538370,Frame = 19,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -3.811903,AccelerationY = 0.995966,AccelerationZ = 2.150767,Frame = 20,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.669073,AccelerationY = 5.087915,AccelerationZ = 0.560163,Frame = 21,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.326504,AccelerationY = 4.735940,AccelerationZ = -2.075322,Frame = 22,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = 0.024796,AccelerationY = 3.905761,AccelerationZ = -2.251118,Frame = 23,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -0.922955,AccelerationY = 2.735532,AccelerationZ = -0.015804,Frame = 24,TrainingSessionId = 1},
            //     //     new MomentarilyAcceleration(){AccelerationX = -1.609328,AccelerationY = 1.874404,AccelerationZ = 1.759468,Frame = 24,TrainingSessionId = 1}
            //     //     );
            //
            //     context.SaveChanges();
            // }
            else
            {
                Console.WriteLine("--> we already have data");
            }
        }
    }
}