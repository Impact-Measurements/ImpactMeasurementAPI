using System.Collections;
using System.Collections.Generic;

namespace ImpactMeasurementAPI.Data
{
    public interface IFreeAccelerationRepo
    {
        bool SaveChanges();

        IEnumerable<double> GetAllFreeAccelerationValuesFromSession(int id);

        double GetHighestForceOfImpactFromSession(int id);

        IEnumerable<double> GetAllImpactDataFromSession(int id);

        double GetAverageForceOfImpactFromSession(int id);
    }
}