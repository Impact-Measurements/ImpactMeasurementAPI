using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateImpact
    {
       
        private readonly double _mass;
        private readonly List<MomentarilyAcceleration> _momentarilyAccelerations;
        private List<Impact> impacts;

        private readonly double _minimumImpactThreshold;
        private readonly double _highImpactThreshold;
        private readonly double _mediumImpactThreshold;

        public CalculateImpact(List<MomentarilyAcceleration> momentarilyAccelerations, double mass)
        {
            _momentarilyAccelerations = momentarilyAccelerations;
            _mass = mass;
        }
        
        public CalculateImpact(List<MomentarilyAcceleration> momentarilyAccelerations, double mass, double minimumImpactThreshold)
        {
            _momentarilyAccelerations = momentarilyAccelerations;
            _mass = mass;
            _minimumImpactThreshold = minimumImpactThreshold;
        }

        public CalculateImpact(List<MomentarilyAcceleration> momentarilyAccelerations, Athlete user)
        {
            _momentarilyAccelerations = momentarilyAccelerations;
            _mass = user.Mass;
            _highImpactThreshold = user.HighImpactThreshold;
            _mediumImpactThreshold = user.MediumImpactThreshold;
        }

        public IEnumerable<Impact> CalculateAllImpacts()
        {
            impacts = new List<Impact>();

            //acceleration value to compare to the next value to detect impact
            double accelerationZ = 0;
            double accelerationY = 0;
            double accelerationX = 0;
            int frame = 0;

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
                    frame = value.Frame;
                }

                //If the acceleration doesn't increase anymore, there will be impact
                //When the acceleration hits 0 or above, there was or will be a point of impact and we need to add
                //that to the list
                if (accelerationZ < 0 && value.AccelerationZ > accelerationZ && value.AccelerationZ>= 0)
                { 
                    var impactZ = accelerationZ * _mass * -1;
                    var impactY = accelerationY * _mass * -1;
                    var impactX = accelerationX * _mass * -1;

                    //Total impact is the Resultant Force. Resultant force is calculated by using the Pythagorean Theorem twice
                    var totalImpact = Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow(impactX, 2) + Math.Pow(impactY, 2)), 2) +
                                                Math.Sqrt(Math.Pow(impactZ, 2)));

                    //if minimum impact is defined and total impact is above the threshold, add it to the list.
                    //if minimum impact is not defined, add total impact to the list.
                    if (!(_minimumImpactThreshold >= 1) || !(totalImpact < _minimumImpactThreshold))
                    {
                        Impact impact = new Impact()
                        {
                            ImpactForce = totalImpact, ImpactDirectionX = impactX, ImpactDirectionY = impactY,
                            ImpactDirectionZ = impactZ, Frame = frame
                        };

                        //Register the color of the impact (red is bad, yellow is medium, green is good)
                        //If statement can be turned around if it turns out that more high impact forces are more common
                        if (totalImpact < _mediumImpactThreshold)
                        {
                            impact.Color = Color.GREEN;
                        }
                        else if (totalImpact < _highImpactThreshold)
                        {
                            impact.Color = Color.YELLOW;
                        }
                        else
                        {
                            impact.Color = Color.RED;
                        }

                        impacts.Add(impact);
                    }

                    accelerationZ = 0;
                    accelerationY = 0;
                    accelerationX = 0;
                }
            }

            return impacts;
        }
        
    }
}