using System;
using System.Collections.Generic;
using System.Text.Json;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using NUnit.Framework;

namespace TestProject.UnitTests
{
    public class Tests
    {
        private TrainingSession trainingSession;
        private List<MomentarilyAcceleration> momentarilyAccelerations;
        
        [SetUp]
        public void Setup()
        {
            trainingSession = new TrainingSession();
            MomentarilyAcceleration ma1 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.009732, AccelerationY = 0.001116,
                AccelerationZ = 0.067139, Frame = 0
            };
            MomentarilyAcceleration ma2 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.012595, AccelerationY = -0.006085,
                AccelerationZ = 0.058178, Frame = 1
            };
            MomentarilyAcceleration ma3 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.013917, AccelerationY = -0.005619,
                AccelerationZ = 0.063357, Frame = 2
            };
            MomentarilyAcceleration ma4 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.017616, AccelerationY = 0.005101,
                AccelerationZ = 0.069366, Frame = 3
            };
            MomentarilyAcceleration ma5 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.411952, AccelerationY = 0.006902,
                AccelerationZ = 0.064761, Frame = 4
            };
            MomentarilyAcceleration ma6 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.013877, AccelerationY = 0.010756,
                AccelerationZ = 0.066491, Frame = 5
            };
            MomentarilyAcceleration ma7 = new MomentarilyAcceleration()
            {
                AccelerationX = -0.009217, AccelerationY = -0.005916,
                AccelerationZ = 0.056226, Frame = 6
            };
            MomentarilyAcceleration ma8 = new MomentarilyAcceleration()
            {
                AccelerationX = 1.373804, AccelerationY = -3.055712,
                AccelerationZ = -5.190740, Frame = 7
            };
            MomentarilyAcceleration ma9 = new MomentarilyAcceleration()
            {
                AccelerationX = 2.627385, AccelerationY = -1.833706,
                AccelerationZ = -3.758188, Frame = 8
            };
            MomentarilyAcceleration ma10 = new MomentarilyAcceleration()
            {
                AccelerationX = 4.302648, AccelerationY = 1.018646,
                AccelerationZ = -1.958635, Frame = 9
            };
            MomentarilyAcceleration ma11 = new MomentarilyAcceleration()
            {
                AccelerationX = 4.978043, AccelerationY = 3.547089,
                AccelerationZ = -1.604290, Frame = 10
            };
            MomentarilyAcceleration ma12 = new MomentarilyAcceleration()
            {
                AccelerationX = 4.182585, AccelerationY = -0.729916,
                AccelerationZ = 0.832159, Frame = 11
            };
            MomentarilyAcceleration ma13 = new MomentarilyAcceleration()
            {
                AccelerationX = -5.476443, AccelerationY = -9.666263,
                AccelerationZ = 7.893936, Frame = 12
            };

            momentarilyAccelerations = new List<MomentarilyAcceleration>();
            momentarilyAccelerations.Add(ma1);
            momentarilyAccelerations.Add(ma2);
            momentarilyAccelerations.Add(ma3);
            momentarilyAccelerations.Add(ma4);
            momentarilyAccelerations.Add(ma5);
            momentarilyAccelerations.Add(ma6);
            momentarilyAccelerations.Add(ma7);
            momentarilyAccelerations.Add(ma8);
            momentarilyAccelerations.Add(ma9);
            momentarilyAccelerations.Add(ma10);
            momentarilyAccelerations.Add(ma11);
            momentarilyAccelerations.Add(ma12);
            momentarilyAccelerations.Add(ma13);
            
            

            trainingSession.FreeAcceleration = momentarilyAccelerations;

            foreach (var i in momentarilyAccelerations)
            {
                i.TrainingSessionId = trainingSession.Id;
            }

        }

        [Test]
        public void Test1()
        {
            Setup();
            CalculateImpact calculateImpact = new CalculateImpact(momentarilyAccelerations, 75);
            foreach (var impact in calculateImpact.CalculateAllImpacts())
            {
                Console.WriteLine(impact.ImpactForce);
            }
            
        }
    }
}