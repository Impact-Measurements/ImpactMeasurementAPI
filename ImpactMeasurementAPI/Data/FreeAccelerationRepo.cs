using System;
using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImpactMeasurementAPI.Data
{
    //TODO
    public class FreeAccelerationRepo : IFreeAccelerationRepo
    {
        private readonly AppDbContext _context;

        public FreeAccelerationRepo(AppDbContext context)
        {
            _context = context;
        }
        
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<MomentarilyAcceleration> GetAllFreeAccelerationValuesFromSession(int id)
        {
            return _context.MomentarilyAccelerations
                .Where(c => c.TrainingSessionId == id)
                .ToList();
        }

        public Impact GetHighestForceOfImpactFromSession(int id)
        {
            return GetAllImpactDataFromSession(id)
                .OrderBy(i => i.ImpactForce)
                .FirstOrDefault();
        }

        public IEnumerable<Impact> GetAllImpactDataFromSession(int id)
        {
            TrainingSession trainingSession = GetTrainingSession(id);
            if (trainingSession == null)
            {
                throw new ArgumentNullException(nameof(trainingSession));
            }
            
            var momentarilyAccelerations = GetAllFreeAccelerationValuesFromSession(id);

            CalculateImpact calculateImpact = new CalculateImpact(momentarilyAccelerations.ToList(), 75);
            return calculateImpact.CalculateAllImpacts();
        }

        public IEnumerable<Impact> GetAllImpactDataFromSession(int id, double minimumThreshold)
        {
            TrainingSession trainingSession = GetTrainingSession(id);
            if (trainingSession == null)
            {
                throw new ArgumentNullException(nameof(trainingSession));
            }
            
            var momentarilyAccelerations = GetAllFreeAccelerationValuesFromSession(id);

            CalculateImpact calculateImpact = new CalculateImpact(momentarilyAccelerations.ToList(), 75, minimumThreshold);
            return calculateImpact.CalculateAllImpacts();
        }
        
        public IEnumerable<Impact> GetAllImpactDataFromImpactZone(int id, string zone)
        {
            var trainingSession = GetTrainingSession(id);
            if (trainingSession == null)
            {
                throw new ArgumentNullException(nameof(trainingSession));
            }
            
            var momentarilyAccelerations = GetAllFreeAccelerationValuesFromSession(id);

            var calculateImpact = new CalculateImpact(momentarilyAccelerations.ToList(), 75);

            return zone switch
            {
                "low" => calculateImpact.CalculateAllImpacts().Where(d => d.Color == Color.GREEN),
                "medium" => calculateImpact.CalculateAllImpacts().Where(d => d.Color == Color.YELLOW),
                "high" => calculateImpact.CalculateAllImpacts().Where(d => d.Color == Color.RED),
                _ => calculateImpact.CalculateAllImpacts()
            };
        }

        public double GetAverageForceOfImpactFromSession(int id)
        {
            var impactFromTrainingSession = GetAllImpactDataFromSession(id);

            if (impactFromTrainingSession != null && impactFromTrainingSession.Count() != 0)
            {
                return impactFromTrainingSession.Average(d => d.ImpactForce);
            }

            throw new ArgumentNullException();
        }

        public TrainingSession GetTrainingSession(int id)
        {
            return _context.TrainingSessions.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TrainingSession> GetAllTrainingSessions(int userId)
        {
            return _context.TrainingSessions.Where(i => i.UserId == userId).ToList();
        }

        public IEnumerable<TrainingSession> GetAllTrainingSessions()
        {
            return _context.TrainingSessions.ToList();
        }

        public void CreateTrainingSession(TrainingSession trainingSession)
        {
            if (trainingSession == null)
            {
                throw new ArgumentNullException(nameof(trainingSession));
            }

            _context.TrainingSessions.Add(trainingSession);
        }

        public void CreateMomentarilyAcceleration(MomentarilyAcceleration momentarilyAcceleration)
        {
            if (momentarilyAcceleration == null)
            {
                throw new ArgumentNullException(nameof(momentarilyAcceleration));
            }

            _context.MomentarilyAccelerations.Add(momentarilyAcceleration);
        }
    }
}