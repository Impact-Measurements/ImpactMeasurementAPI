using System;
using System.Collections.Generic;

namespace ImpactMeasurementAPI.Models
{
    public class TrainingSession
    {
        public int Id;

        public List<MomentarilyAcceleration> FreeAcceleration;

        public DateTime StartingTime;

        public Sport Sport;

        public Coach Coach;
    }
}