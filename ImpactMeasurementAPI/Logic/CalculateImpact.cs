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
            double accelerationZ = 0;
            double accelerationY = 0;
            double accelerationX = 0;
            int frame = 0;

            if (_momentarilyAccelerations == null) return null;

            foreach (var value in _momentarilyAccelerations)
            {
                UpdateMaxAcceleration(value, ref accelerationZ, ref accelerationY, ref accelerationX, ref frame);
                DetectImpactAndUpdateList(value, accelerationZ, ref accelerationZ, ref accelerationY, ref accelerationX, frame);
            }

            return impacts;
        }

        private void UpdateMaxAcceleration(MomentarilyAcceleration value, ref double accelerationZ, ref double accelerationY, ref double accelerationX, ref int frame)
        {
            if (value.AccelerationZ < accelerationZ)
            {
                accelerationZ = value.AccelerationZ;
                accelerationY = value.AccelerationY;
                accelerationX = value.AccelerationX;
                frame = value.Frame;
            }
        }

        private void DetectImpactAndUpdateList(MomentarilyAcceleration value, double accelerationZ, ref double maxAccelerationZ, ref double accelerationY, ref double accelerationX, int frame)
        {
            if (accelerationZ < 0 && value.AccelerationZ > accelerationZ && value.AccelerationZ >= 0)
            {
                var impactZ = accelerationZ * _mass * -1;
                var impactY = accelerationY * _mass * -1;
                var impactX = accelerationX * _mass * -1;

                var totalImpact = CalculateTotalImpact(impactX, impactY, impactZ);

                if (!(_minimumImpactThreshold >= 1) || !(totalImpact < _minimumImpactThreshold))
                {
                    Impact impact = CreateImpact(totalImpact, impactX, impactY, impactZ, frame);
                    RegisterImpactColor(impact);
                    impacts.Add(impact);
                }

                ResetMaxAcceleration(ref maxAccelerationZ, ref accelerationY, ref accelerationX);
            }
        }

        private double CalculateTotalImpact(double impactX, double impactY, double impactZ)
        {
            return Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow(impactX, 2) + Math.Pow(impactY, 2)), 2) +
                             Math.Sqrt(Math.Pow(impactZ, 2)));
        }

        private Impact CreateImpact(double totalImpact, double impactX, double impactY, double impactZ, int frame)
        {
            return new Impact()
            {
                ImpactForce = totalImpact,
                ImpactDirectionX = impactX,
                ImpactDirectionY = impactY,
                ImpactDirectionZ = impactZ,
                Frame = frame
            };
        }

        private void RegisterImpactColor(Impact impact)
        {
            if (impact.ImpactForce < _mediumImpactThreshold)
            {
                impact.Color = Color.GREEN;
            }
            else if (impact.ImpactForce < _highImpactThreshold)
            {
                impact.Color = Color.YELLOW;
            }
            else
            {
                impact.Color = Color.RED;
            }
        }

        private void ResetMaxAcceleration(ref double maxAccelerationZ, ref double accelerationY, ref double accelerationX)
        {
            maxAccelerationZ = 0;
            accelerationY = 0;
            accelerationX = 0;
        }


    }
}