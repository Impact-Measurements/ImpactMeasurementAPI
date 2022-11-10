using System.Collections.Generic;
using CsvHelper.Configuration;

namespace ImpactMeasurementAPI.Models
{
    public class CsvDataMap : ClassMap<CsvData>
    {
        public CsvDataMap()
        {
            Map(p => p.packetCounter).Index(0);
            Map(p => p.sampleTimeFine).Index(1); 
           
            // Map(p => p.Euler_X).Index(2); 
            // Map(p => p.Euler_Y).Index(3); 
            // Map(p => p.Euler_Z).Index(4); 
            
            Map(p => p.FreeAcc_X).Index(2);
            Map(p => p.FreeAcc_Y).Index(3); 
            Map(p => p.FreeAcc_Z).Index(4);
            //
            // Map(p => p.Gyr_X).Index(8);
            // Map(p => p.Gyr_Y).Index(9); 
            // Map(p => p.Gyr_Z).Index(10); 

        }
    }
}