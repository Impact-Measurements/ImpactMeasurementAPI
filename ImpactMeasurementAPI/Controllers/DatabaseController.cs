using System;
using Npgsql;
using System.Data;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Controllers
{
    public class DatabaseController
    {
        
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(
                @"Server=localhost;Port=5432;User Id=postgres;Password=<>;Database=Xsens-data");
        }
        
        public static void TestConnection()
        {
            using (NpgsqlConnection con=GetConnection())
            {
                con.Open();
                if (con.State==ConnectionState.Open)
                {
                    Console.WriteLine("Connected to database");
                }
                else
                {
                    Console.WriteLine("Not connected");
                }
            }
        }

        public static void InsertRecord(CsvData data)
        {
            using (NpgsqlConnection con=GetConnection())
            {
                string query =
                    $@"insert into public.training(PacketCounter,SampleTimeFine,dq_W,dq_X,dq_Y,dq_Z,dv_1,dv_2,dv_3,Mag_X,Mag_Y,Mag_Z,Status)values({data.packetCounter},{data.sampleTimeFine},{data.dq_w},{data.dq_x},{data.dq_y},{data.dq_z},{data.dv_1},{data.dv_2},{data.dv_3},{data.mag_x},{data.mag_y},{data.mag_z},{data.status}) ";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n == 1)
                {
                    Console.WriteLine("Record Inserted");
                }
            }
        }
    }
}