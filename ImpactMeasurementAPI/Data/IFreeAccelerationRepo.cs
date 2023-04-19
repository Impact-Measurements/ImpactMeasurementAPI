using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Data
{
    public interface IFreeAccelerationRepo
    {
        bool SaveChanges();

        IEnumerable<MomentarilyAcceleration> GetAllFreeAccelerationValuesFromSession(int id);

        Impact GetHighestForceOfImpactFromSession(int id);

        IEnumerable<Impact> GetAllImpactDataFromSession(int id);
        IEnumerable<Impact> GetAllImpactDataFromSession(int id, double minimumThreshold);
        IEnumerable<Impact> GetAllImpactDataFromImpactZone(int id, string zone);

        double GetAverageForceOfImpactFromSession(int id);

        TrainingSession GetTrainingSession(int id);

        IEnumerable<TrainingSession> GetAllTrainingSessions(int userId);
        IEnumerable<TrainingSession> GetAllTrainingSessions();

        void CreateTrainingSession(TrainingSession trainingSession);

        void CreateMomentarilyAcceleration(MomentarilyAcceleration momentarilyAcceleration);
    }
}