using CsvHelper.Configuration.Attributes;

namespace ImpactMeasurementAPI.Models
{
    public class CsvData
    {
        [Index(0)] public float PacketCounter { get; set; }

        [Index(1)] public float SampleTimeFine { get; set; }

        //Euler
        // [Index(2)]
        // public float Euler_X { get; set; }
        // [Index(3)]
        // public float Euler_Y { get; set; }
        // [Index(4)]
        // public float Euler_Z { get; set; }
        //FreeAcc
        [Index(6)] public float FreeAcc_X { get; set; }

        [Index(7)] public float FreeAcc_Y { get; set; }

        [Index(8)] public float FreeAcc_Z { get; set; }
        //Mag
        // [Index(9)]
        // public float Gyr_X { get; set; }
        // [Index(10)]
        // public float Gyr_Y { get; set; }
        // [Index(11)]
        // public float Gyr_Z { get; set; }
    }
}