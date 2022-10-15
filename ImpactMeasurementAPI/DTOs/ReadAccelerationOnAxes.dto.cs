using System.Collections.Generic;

namespace ImpactMeasurementAPI.DTOs
{
    public class ReadAccelerationOnAxes
    {
        public List<double> FreeAccelerationX { get; set; }
        public List<double> FreeAccelerationY { get; set; }
        public List<double> FreeAccelerationZ { get; set; }
    }
}