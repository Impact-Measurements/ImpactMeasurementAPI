using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateThresholds
    {
        private TrainingSession _trainingSession;
        private User _user;
        
        public CalculateThresholds(TrainingSession trainingSession, User user)
        {
            _trainingSession = trainingSession;
            _user = user;
        }

        private void CalculateAllThresholds()
        {
            double highestImpact;
            double lowestImpact;
            
            highestImpact = _trainingSession.Impacts.OrderByDescending(d => d.ImpactForce).FirstOrDefault().ImpactForce;
            lowestImpact = _trainingSession.Impacts.OrderBy(d => d.ImpactForce).FirstOrDefault().ImpactForce;

            if (_trainingSession.PainfulnessScore <= 2)
            {
                
            } else if (_trainingSession.PainfulnessScore < 7)
            {
                
            }
            else
            {
                
            }
        }
    }
}