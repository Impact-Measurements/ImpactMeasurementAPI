using System.Collections;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateImpact
    {
        private readonly FreeAcceleration _x;
        private readonly FreeAcceleration _y;
        private readonly FreeAcceleration _z;
        private readonly double _mass;


        public CalculateImpact(TrainingSession trainingSession, double mass)
        {
            _x = trainingSession.FreeAccelerationX;
            _y = trainingSession.FreeAccelerationY;
            _z = trainingSession.FreeAccelerationZ;
            _mass = mass;
        }

        public IEnumerable<double> CalculateAllImpacts()
        {
            var impacts = new List<double>();
            
            //acceleration value to compare to the next value to detect impact
            double a1 = 0;

            if (_x == null) return null;
            
            //For each acceleration value, see if the acceleration is going towards the ground
            foreach (var value in _z.Values)
            {
                //If the acceleration towards the ground is still increasing,
                //set a1 to the lowers value (=highest acceleration to the ground)
                if (value < a1)
                {
                    a1 = value;
                }

                //If the acceleration doesn't increase anymore, there will be impact
                //When the acceleration hits 0 or above, there was or will be a point of impact and we need to add
                //that to the list
                if (!(a1 < 0) || !(value > a1) || !(value >= 0)) continue;
                impacts.Add(-1*a1*_mass);
                a1 = 0;

            }

            return impacts;
        }
    }
}