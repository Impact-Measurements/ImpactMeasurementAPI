using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Controllers
{
    public class CsvController
    {
        public static void OpenFile()
        {
            
            DatabaseController.TestConnection();
            Console.ReadKey();
            
            using (var streamReader =
                   new StreamReader(Path.Combine(Environment.CurrentDirectory, "ImpactMeasurementAPI/TrainingFiles/Xsens DOT_20220915_113534_584.csv")))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<CsvDataMap>();
                    var records = csvReader.GetRecords<CsvData>().ToList();
                    
                    // Console.WriteLine(records[0].packetCounter);
                    // Console.WriteLine(records[10].packetCounter);

                    var count = 0;
                    foreach (var record in records)
                    {
                        DatabaseController.InsertRecord(record);
                        
                        count++;
                        Console.WriteLine(count);
                        
                    }

                }
            }
        }
        
        public static void ReadData()
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Comment = '#',
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader("filePersons.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<CsvDataMap>();
                var data = csv.GetRecords<CsvData>();
            }

        }
    }
}