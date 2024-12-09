using System;
using System.Data.SqlClient;

namespace KlasseLib.KlasseKontrolServices
{
    public class SensorDB : ISensorDB
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        // Tilføjer en sensor til databasen
        public void AddSensor(string sensorType, double? temperatureValue, double? soundValue, DateTime lastMeasurement)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    const string query = @"
                        INSERT INTO Sensors (SensorType, TemperatureValue, SoundValue, LastMeasurement)
                        VALUES (@SensorType, @TemperatureValue, @SoundValue, @LastMeasurement)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SensorType", sensorType);
                        command.Parameters.AddWithValue("@TemperatureValue", (object)temperatureValue ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SoundValue", (object)soundValue ?? DBNull.Value);
                        command.Parameters.AddWithValue("@LastMeasurement", lastMeasurement);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Sensor added to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding sensor: {ex.Message}");
                throw; // Genkaster exception, hvis den skal håndteres højere oppe
            }
        }

        public List<Sensor> GetAllSensors()
        {
            var sensors = new List<Sensor>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    const string query = "SELECT * FROM Sensors";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = Convert.ToInt32(reader["Id"]);
                                var sensorType = reader["SensorType"].ToString();
                                var temperatureValue = reader["TemperatureValue"] != DBNull.Value
                                    ? (double?)reader["TemperatureValue"]
                                    : null;
                                var soundValue = reader["SoundValue"] != DBNull.Value
                                    ? (double?)reader["SoundValue"]
                                    : null;
                                var lastMeasurement = Convert.ToDateTime(reader["LastMeasurement"]);

                                if (sensorType == "Temperature")
                                {
                                    sensors.Add(new TemperatureSensor(id, sensorType, temperatureValue.GetValueOrDefault(), lastMeasurement));
                                }
                                else if (sensorType == "Sound")
                                {
                                    sensors.Add(new SoundSensor(id, sensorType, soundValue.GetValueOrDefault(), lastMeasurement));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sensors: {ex.Message}");
                throw;
            }

            return sensors;
        }

        // Henter en sensor fra databasen baseret på ID
        public Sensor GetSensorById(int id)
        {
            try
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
                                var sensorType = reader["SensorType"].ToString();
                                var temperatureValue = reader["TemperatureValue"] != DBNull.Value
                                    ? (double?)reader["TemperatureValue"]
                                    : null;
                                var soundValue = reader["SoundValue"] != DBNull.Value
                                    ? (double?)reader["SoundValue"]
                                    : null;
                                var lastMeasurement = Convert.ToDateTime(reader["LastMeasurement"]);

                                if (sensorType == "Temperature")
                                {
                                    return new TemperatureSensor(id, sensorType, temperatureValue.GetValueOrDefault(), lastMeasurement);
                                }
                                else if (sensorType == "Sound")
                                {
                                    return new SoundSensor(id, sensorType, soundValue.GetValueOrDefault(), lastMeasurement);
                                }
                                else
                                {
                                    throw new Exception("Unknown sensor type.");
                                }
                            }
                            else
                            {
                                throw new Exception($"Sensor with ID {id} not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sensor: {ex.Message}");
                throw; // Genkaster exception
            }
        }
    }
    
}
