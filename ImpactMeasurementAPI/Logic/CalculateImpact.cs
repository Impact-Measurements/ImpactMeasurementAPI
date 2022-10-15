using System.Collections;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateImpact
    {
       
        private readonly double _mass;
        private readonly TrainingSession _trainingSession;


        public CalculateImpact(TrainingSession trainingSession, double mass)
        {
            _trainingSession = trainingSession;
            _mass = mass;
        }

        public IEnumerable<double> CalculateAllImpacts()
        {
            var impacts = new List<double>();
            
            //acceleration value to compare to the next value to detect impact
            double a1 = 0;

            if (_trainingSession == null) return null;
            
            //For each acceleration value, see if the acceleration is going towards the ground
            foreach (var value in _trainingSession.FreeAcceleration)
            {
                //If the acceleration towards the ground is still increasing,
                //set a1 to the lowers value (=highest acceleration to the ground)
                if (value.AccelerationZ < a1)
                {
                    a1 = value.AccelerationZ;
                }

                //If the acceleration doesn't increase anymore, there will be impact
                //When the acceleration hits 0 or above, there was or will be a point of impact and we need to add
                //that to the list
                if (!(a1 < 0) || !(value.AccelerationZ > a1) || !(value.AccelerationZ >= 0)) continue;
                impacts.Add(-1*a1*_mass);
                a1 = 0;

            }

            return impacts;
        }
    }
}