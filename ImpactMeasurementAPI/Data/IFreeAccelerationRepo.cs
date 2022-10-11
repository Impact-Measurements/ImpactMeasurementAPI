using System.Collections;
using System.Collections.Generic;

namespace ImpactMeasurementAPI.Data
{
    public interface IFreeAccelerationRepo
    {
        bool SaveChanges();

        IEnumerable<double> GetAllFreeAccelerationValues();

        double GetHighestForceOfImpact();

        IEnumerable<double> GetAllImpactData();

        double GetAverageForceOfImpact();
    }
}