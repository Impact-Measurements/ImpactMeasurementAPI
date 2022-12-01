using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Http;

namespace ImpactMeasurementAPI.Logic
{
    public class CsvController
    {
        public static List<CsvData> ParseCSVString()
        {
            var csv =
                @"PacketCounter,SampleTimeFine,FreeAcc_X,FreeAcc_Y,FreeAcc_Z
1,192856375,0.0098430,.00955,0.056523
2,192873042,0.008296,0.006831,0.046397";

            var textReader = new StringReader(csv);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            };

            var csvr = new CsvReader(textReader, config);


            var tings = csvr.GetRecords<CsvData>().ToList();
            return tings;
        }

        public static string DetectDelimiter(StreamReader reader)
        {
            var possibleDelimiters = new List<string> { ",", ";", "\t", "|" };
            var headerLine = reader.ReadLine();
            reader.BaseStream.Position = 0;
            reader.DiscardBufferedData();
            foreach (var possibleDelimiter in possibleDelimiters)
                if (headerLine.Contains(possibleDelimiter))
                    return possibleDelimiter;
            return possibleDelimiters[0];
        }

        public static List<CsvData> ParseCSVFile(IFormFile csvFile)
        {
            using var memoryStream = new MemoryStream(new byte[csvFile.Length]);
            csvFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                { Delimiter = DetectDelimiter(new StreamReader(memoryStream)) };

            using (var reader = new StreamReader(memoryStream))
            using (var csvReader = new CsvReader(reader, config))
            {
                csvReader.Context.RegisterClassMap<CsvDataMap>();
                var records = csvReader.GetRecords<CsvData>().ToList();

                return records;
            }
        }
    }
}