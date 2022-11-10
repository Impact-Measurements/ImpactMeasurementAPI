using CsvHelper.Configuration.Attributes;

namespace ImpactMeasurementAPI.Models
{
    public class CsvData
    {
        [Index(0)] 
        public int packetCounter { get; set; }
        [Index(1)]
        public float sampleTimeFine { get; set; }
        //Euler
        // [Index(2)]
        // public float Euler_X { get; set; }
        // [Index(3)]
        // public float Euler_Y { get; set; }
        // [Index(4)]
        // public float Euler_Z { get; set; }
        //FreeAcc
        [Index(2)]
        public float FreeAcc_X { get; set; }
        [Index(3)]
        public float FreeAcc_Y { get; set; }
        [Index(4)]
        public float FreeAcc_Z { get; set; }
        //Mag
        // [Index(9)]
        // public float Gyr_X { get; set; }
        // [Index(10)]
        // public float Gyr_Y { get; set; }
        // [Index(11)]
        // public float Gyr_Z { get; set; }
    }
}