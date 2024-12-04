using Microsoft.Data.SqlClient;

namespace KlasseLib.KlasseKontrolServices
{
    public class SensorDB:ISensorDB
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        // Add a sensor to the database
        public void AddSensor(string sensorType, double? temperatureValue, double? soundValue, DateTime lastMeasurement)
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

        // Retrieve a sensor by ID from the database
        public void GetSensorById(int id)
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
                            Console.WriteLine($"ID: {reader["Id"]}, Type: {reader["SensorType"]}, " +
                                              $"Temperature: {reader["TemperatureValue"]}, Sound: {reader["SoundValue"]}, " +
                                              $"LastMeasurement: {reader["LastMeasurement"]}");
                        }
                        else
                        {
                            Console.WriteLine($"Sensor with ID {id} not found.");
                        }
                    }
                }
            }
        }

        // Update a sensor's values and last measurement in the database
        public void UpdateSensor(int id, double? newTemperatureValue, double? newSoundValue)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = @"
                    UPDATE Sensors
                    SET TemperatureValue = @TemperatureValue, SoundValue = @SoundValue, LastMeasurement = @LastMeasurement
                    WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TemperatureValue", (object)newTemperatureValue ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SoundValue", (object)newSoundValue ?? DBNull.Value);
                    command.Parameters.AddWithValue("@LastMeasurement", DateTime.Now);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Sensor with ID {id} updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Sensor with ID {id} not found.");
                    }
                }
            }
        }

        // Delete a sensor by ID from the database
        public void DeleteSensor(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "DELETE FROM Sensors WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Sensor with ID {id} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Sensor with ID {id} not found.");
                    }
                }
            }
        }
    }
}
