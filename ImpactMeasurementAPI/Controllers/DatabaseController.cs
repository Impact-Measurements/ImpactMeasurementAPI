using System;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace ImpactMeasurementAPI.Controllers
{
    public class DatabaseController
    {
        public IConfiguration Configuration { get; }

        public MySqlConnection connection;

        //Constructor
        public DatabaseController()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            var connectionString = "server=db;userid=root;password=my_secret_password;database=test;persistsecurityinfo=True;";
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
        public void SaveTraining(List<CsvData> records)
        {
            //create a new session
            var trainingSessionID = InsertTraining();
            // var trainingSessionID = 1;

            if (trainingSessionID != 0)
                //add each packet for that sessions
                foreach (var record in records)
                {
                    InsertFrame(record, trainingSessionID);
                }

        }

        //Insert a new training moment
        public long InsertTraining()
        {
            string query = $@"INSERT INTO test.TrainingSession (StartingTime) VALUES({DateTime.Now})";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO test.TrainingSessions (StartingTime) VALUES (@1)";
                cmd.Parameters.AddWithValue("@1", DateTime.Now);
                //Execute command
                cmd.ExecuteNonQuery();
            
                long trainingId = cmd.LastInsertedId;
                //close connection
                CloseConnection();
            
                return trainingId;
            }

            return 0;
        }


        //Insert a single frame / packetcount
        public  void InsertFrame(CsvData record, long trainingId)
        {
            string query = $@"INSERT INTO test.MomentarilyAccelerations(TrainingSessionId, Frame, AccelerationX, AccelerationY, AccelerationZ) VALUES({trainingId}, {record.packetCounter}, {record.FreeAcc_X}, {record.FreeAcc_Y}, {record.FreeAcc_Z})";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

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