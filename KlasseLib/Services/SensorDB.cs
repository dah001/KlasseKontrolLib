using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KlasseLib.KlasseKontrolServices
{
    public class SensorDB : ISensorDB
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            var sensors = new List<Sensor>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    string query = @"
                        SELECT Id, 'Sound' AS SensorType, CurrentValue, LastMeasurement FROM SoundSensor
                        UNION ALL
                        SELECT Id, 'Temperature' AS SensorType, CurrentValue, LastMeasurement FROM TemperatureSensor";

                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var sensorType = reader["SensorType"].ToString();
                            if (sensorType == "Sound")
                            {
                                sensors.Add(new SoundSensor(
                                    id: (int)reader["Id"],
                                    sensorType: sensorType,
                                    currentValue: (double)reader["CurrentValue"],
                                    lastMeasurement: (DateTime)reader["LastMeasurement"]
                                ));
                            }
                            else if (sensorType == "Temperature")
                            {
                                sensors.Add(new TemperatureSensor(
                                    id: (int)reader["Id"],
                                    sensorType: sensorType,
                                    currentValue: (double)reader["CurrentValue"],
                                    lastMeasurement: (DateTime)reader["LastMeasurement"]
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllSensorsAsync: {ex.Message}");
                throw;
            }

            return sensors;
        }
    }
}
