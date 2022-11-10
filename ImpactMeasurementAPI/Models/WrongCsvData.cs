using CsvHelper.Configuration.Attributes;

namespace ImpactMeasurementAPI.Models
{
    public class WrongCsvData
    {
        [Index(0)] 
        public int packetCounter { get; set; }
        [Index(1)]
        public float sampleTimeFine { get; set; }
        //dq
        [Index(2)]
        public float dq_w { get; set; }
        [Index(3)]
        public float dq_x { get; set; }
        [Index(4)]
        public float dq_y { get; set; }
        [Index(5)]
        public float dq_z { get; set; }
        //dv
        [Index(6)]
        public float dv_1 { get; set; }
        [Index(7)]
        public float dv_2 { get; set; }
        [Index(8)]
        public float dv_3 { get; set; }
        //Mag
        [Index(9)]
        public float mag_x { get; set; }
        [Index(10)]
        public float mag_y { get; set; }
        [Index(11)]
        public float mag_z { get; set; }
        [Index(12)]
        public int status { get; set; }
    }
}