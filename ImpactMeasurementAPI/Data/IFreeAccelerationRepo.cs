using System.Collections;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Data
{
    public interface IFreeAccelerationRepo
    {
        bool SaveChanges();

        IEnumerable<MomentarilyAcceleration> GetAllFreeAccelerationValuesFromSession(int id);

        double GetHighestForceOfImpactFromSession(int id);

        IEnumerable<Impact> GetAllImpactDataFromSession(int id);

        double GetAverageForceOfImpactFromSession(int id);

        TrainingSession GetTrainingSession(int id);

    }
}