using System;
using System.Collections;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateImpact
    {
       
        private readonly double _mass;
        private readonly List<MomentarilyAcceleration> _momentarilyAccelerations;
        private List<Impact> impacts;

        public CalculateImpact(List<MomentarilyAcceleration> momentarilyAccelerations, double mass)
        {
            _momentarilyAccelerations = momentarilyAccelerations;
            _mass = mass;
        }

        public IEnumerable<Impact> CalculateAllImpacts()
        {
            impacts = new List<Impact>();
            
            //acceleration value to compare to the next value to detect impact
            double accelerationZ = 0;
            double accelerationY = 0;
            double accelerationX = 0;

            if (_momentarilyAccelerations == null) return null;
            
            //For each acceleration value, see if the acceleration is going towards the ground
            foreach (var value in _momentarilyAccelerations)
            {
                //If the acceleration towards the ground is still increasing,
                //set a1 to the lowers value (=highest acceleration to the ground)
                if (value.AccelerationZ < accelerationZ)
                {
                    accelerationZ = value.AccelerationZ;
                    accelerationY = value.AccelerationY;
                    accelerationX = value.AccelerationX;
                }

                //If the acceleration doesn't increase anymore, there will be impact
                //When the acceleration hits 0 or above, there was or will be a point of impact and we need to add
                //that to the list
                if (accelerationZ < 0 && value.AccelerationZ > accelerationZ && value.AccelerationZ>= 0)
                { 
                    var impactZ = accelerationZ * _mass;
                    var impactY = accelerationY * _mass;
                    var impactX = accelerationX * _mass;
                
                    //Total impact is the Resultant Force. Resultant force is calculated by using the Pythagorean Theorem twice
                    var totalImpact = Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow(impactX, 2) + Math.Pow(impactY, 2)), 2) +
                                                Math.Sqrt(Math.Pow(impactZ, 2)));
                
                    impacts.Add(new Impact()
                    {
                        ImpactForce = totalImpact, ImpactDirectionX = impactX, ImpactDirectionY = impactY
                        ,ImpactDirectionZ = impactZ
                    });

                    accelerationZ = 0;
                    accelerationY = 0;
                    accelerationX = 0;
                }
            }

            return impacts;
        }
        
    }
}