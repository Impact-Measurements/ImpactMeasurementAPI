using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;

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

        public IEnumerable<double> GetAllFreeAccelerationValuesFromSession(int id)
        {
            var readAccelerationOnAxes = new ReadAccelerationOnAxes();
            var trainingSession = new TrainingSession();

            trainingSession = _context.TrainingSessions
                .FirstOrDefault(p => p.Id == id);
            
            throw new System.NotImplementedException();
            
        }

        public double GetHighestForceOfImpactFromSession(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<double> GetAllImpactDataFromSession(int id)
        {
            throw new System.NotImplementedException();
        }

        public double GetAverageForceOfImpactFromSession(int id)
        {
            throw new System.NotImplementedException();
        }

        public TrainingSession GetTrainingSession(int id)
        {
            var trainingSession = new TrainingSession();

            trainingSession = _context.TrainingSessions
                .FirstOrDefault(p => p.Id == id);

            return trainingSession;
        }
    }
}