using System;
using System.Collections.Generic;
using System.Globalization;
using ImpactMeasurementAPI.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ImpactMeasurementAPI.Logic
{
    public class DatabaseController
    {
        public MySqlConnection connection;

        //Constructor
        public DatabaseController()
        {
            Initialize();
        }

        public IConfiguration Configuration { get; }

        //Initialize values
        private void Initialize()
        {
            var connectionString =
                "Server=host.docker.internal;port=3306;Database=test;Uid=root;Pwd=my_secret_password;";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }

                return false;
            }
        }

        // Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement


        //save all training data
        public long SaveTraining(List<CsvData> records, int user, int effect, int pain)
        {
            //create a new session
            var trainingSessionID = InsertTraining(user, effect, pain);
            // var trainingSessionID = 1;

            if (trainingSessionID != 0)
                //add each packet for that sessions
                foreach (var record in records)
                {
                    InsertFrame(record, trainingSessionID);
                }
            return trainingSessionID;
        }

        //Insert a new training moment
        public long InsertTraining(int userId, int effect, int pain)
        {
            // var query = $@"INSERT INTO test.TrainingSession (StartingTime) VALUES({user.Id},{DateTime.Now},)";

            //open connection
            if (OpenConnection())
            {
                //create command and assign the query and connection from the constructor
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO test.TrainingSessions (UserId, StartingTime, EffectivenessScore, PainfulnessScore) VALUES (@1,@2, @3, @4)";
                cmd.Parameters.AddWithValue("@1", userId);
                cmd.Parameters.AddWithValue("@2", DateTime.Now);
                cmd.Parameters.AddWithValue("@3", effect);
                cmd.Parameters.AddWithValue("@4", pain);
                
                //Execute command
                cmd.ExecuteNonQuery();

                var trainingId = cmd.LastInsertedId;
                //close connection
                CloseConnection();

                return trainingId;
            } 

            return 0;
        }


        //Insert a single frame / packetcount
        public void InsertFrame(CsvData record, long trainingId)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            var query =
                $@"INSERT INTO test.MomentarilyAccelerations(TrainingSessionId, Frame, AccelerationX, AccelerationY, AccelerationZ) VALUES({trainingId}, {record.PacketCounter}, {record.FreeAcc_X.ToString(culture)}, {record.FreeAcc_Y.ToString(culture)}, {record.FreeAcc_Z.ToString(culture)})";

            //open connection
            if (OpenConnection())
            {
                //create command and assign the query and connection from the constructor
                var cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        // public List <string> [] Select()
        // {
        // }

        //Count statement
        // public int Count()
        // {
        //     
        // }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}