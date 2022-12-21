using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Logic
{
    public class CalculateThresholds
    {
        private TrainingSession _trainingSession;
        private Athlete _user;
        
        public CalculateThresholds(TrainingSession trainingSession, Athlete user)
        {
            _trainingSession = trainingSession;
            _user = user;
        }

        private void CalculateAllThresholds()
        {
            var allImpact = _trainingSession.Impacts.OrderByDescending(d => d.ImpactForce).ToList();
            var averageImpact = allImpact.Average(d => d.ImpactForce);
            var highestImpact = _trainingSession.Impacts.OrderByDescending(d => d.ImpactForce).FirstOrDefault()!.ImpactForce;
            var lowestImpact = _trainingSession.Impacts.OrderBy(d => d.ImpactForce).FirstOrDefault()!.ImpactForce;

            if (_user.HighImpactThreshold <= 1 && _user.MediumImpactThreshold <= 1)
            {
                switch (_trainingSession.PainfulnessScore)
                {
                    case <= 2:
                        _user.MediumImpactThreshold = highestImpact;
                        break;
                    case < 7:
                        _user.MediumImpactThreshold = (lowestImpact + averageImpact) / 2;
                        _user.HighImpactThreshold = averageImpact;
                        break;
                    default:
                        _user.MediumImpactThreshold = averageImpact;
                        _user.HighImpactThreshold = highestImpact;
                        break;
                }
            }
            else
            {
                switch (_trainingSession.PainfulnessScore)
                {
                    case <= 2:
                        _user.MediumImpactThreshold = (_user.MediumImpactThreshold + highestImpact)/2;
                        break;
                    case < 7:
                        _user.MediumImpactThreshold = (_user.MediumImpactThreshold + ((lowestImpact + averageImpact) / 2))/2;
                        _user.HighImpactThreshold = (_user.HighImpactThreshold + averageImpact)/2;
                        break;
                    default:
                        _user.MediumImpactThreshold = (_user.MediumImpactThreshold + averageImpact)/2;
                        _user.HighImpactThreshold = (_user.HighImpactThreshold + highestImpact) /2;
                        break;
                }
            }

        }
    }
}