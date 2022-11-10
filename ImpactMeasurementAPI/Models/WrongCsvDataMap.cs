using CsvHelper.Configuration;

namespace ImpactMeasurementAPI.Models
{
    public class WrongCsvDataMap : ClassMap<WrongCsvData>
    {
        public WrongCsvDataMap()
        {
            Map(p => p.packetCounter).Index(0);
            Map(p => p.sampleTimeFine).Index(1); 
           
            Map(p => p.dq_w).Index(2); 
            Map(p => p.dq_x).Index(3); 
            Map(p => p.dq_y).Index(4); 
            Map(p => p.dq_z).Index(5); 

            Map(p => p.dv_1).Index(6); 
            Map(p => p.dv_2).Index(7); 
            Map(p => p.dv_3).Index(8); 

            Map(p => p.mag_x).Index(9); 
            Map(p => p.mag_y).Index(10); 
            Map(p => p.mag_z).Index(11); 

            Map(p => p.status).Index(12); 

        }
    }
}