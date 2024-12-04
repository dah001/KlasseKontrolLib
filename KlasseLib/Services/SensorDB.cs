using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;



namespace KlasseLib.KlasseKontrolRepository
{
    public class SensorDB(ISensorDB.ISqlDatabaseConnection sqlDatabaseConnection) : ISensorDB
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test_thread;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // Add a sensor to the database
        public void AddSensor(Sensor sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException(nameof(sensor));

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "INSERT INTO Sensors (SensorType, CurrentValue, LastMeasurement) VALUES (@SensorType, @CurrentValue, @LastMeasurement)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SensorType", sensor.SensorType);
                    command.Parameters.AddWithValue("@CurrentValue", sensor.CurrentValue);
                    command.Parameters.AddWithValue("@LastMeasurement", sensor.LastMeasurement);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Sensor with ID {sensor.Id} added to the database.");
        }

        // Retrieve a sensor by ID from the database
        public Sensor GetSensorById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Sensors WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Sensor
                            {
                                Id = reader.GetInt32(0),
                                SensorType = reader.GetString(1),
                                CurrentValue = reader.GetDouble(2),
                                LastMeasurement = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            Console.WriteLine($"Sensor with ID {id} not found.");
            return null;
        }

        // Update a sensor's value and last measurement in the database
        public Sensor UpdateSensor(int id, double newValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "UPDATE Sensors SET CurrentValue = @CurrentValue, LastMeasurement = @LastMeasurement WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentValue", newValue);
                    command.Parameters.AddWithValue("@LastMeasurement", DateTime.Now);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Sensor with ID {id} updated successfully.");
                        return GetSensorById(id);
                    }
                    else
                    {
                        Console.WriteLine($"Sensor with ID {id} not found.");
                        return null;
                    }
                }
            }
        }

        // Delete a sensor by ID from the database
        public Sensor DeleteSensor(int id)
        {
            var sensorToDelete = GetSensorById(id);

            if (sensorToDelete == null)
                throw new KeyNotFoundException($"Sensor with ID {id} not found.");

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "DELETE FROM Sensors WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Sensor with ID {id} deleted successfully.");
            return sensorToDelete;
        }
    }

    // Example model class for Sensor
    public class Sensor
    {
        public int Id { get; set; }
        public string SensorType { get; set; }
        public double CurrentValue { get; set; }
        public DateTime LastMeasurement { get; set; }
    }
}
