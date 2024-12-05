using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.KlasseKontrolServices;
using System;
using System.Data.SqlClient;

namespace KlasseLib.KlasseKontrolServices.Tests
{
    [TestClass()]
    public class SensorDBTests
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        [TestMethod()]
        public void AddSensorTest()
        {
            // Arrange
            var sensorDB = new SensorDB();
            var sensorType = "Temperature Sensor";
            var temperatureValue = 22.5;
            var soundValue = (double?)null;
            var lastMeasurement = DateTime.Now;

            int initialCount;

            // Act: Count sensors before adding the new one
            using (var connection = new SqlConnection("Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;"))
            {
                const string countQuery = "SELECT COUNT(*) FROM Sensors";
                using (var countCommand = new SqlCommand(countQuery, connection))
                {
                    connection.Open();
                    initialCount = (int)countCommand.ExecuteScalar(); // Get initial count
                }
            }

            // Add the new sensor
            sensorDB.AddSensor(sensorType, temperatureValue, soundValue, lastMeasurement);

            // Assert: Check the count after adding the sensor
            using (var connection = new SqlConnection("Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;"))
            {
                const string countQuery = "SELECT COUNT(*) FROM Sensors";
                using (var countCommand = new SqlCommand(countQuery, connection))
                {
                    connection.Open();
                    var finalCount = (int)countCommand.ExecuteScalar(); // Get count after adding

                    // Assert that the count has increased by 1
                    Assert.AreEqual(initialCount + 1, finalCount, "Sensor was not added to the database.");
                }
            }
        }

        [TestMethod()]
        public void GetSensorByIdTest()
        {
            // Arrange
            var sensorDB = new SensorDB();
            int sensorId =  3; // Replace with a valid sensor ID for your test

            // Act & Assert
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT COUNT(*) FROM Sensors WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sensorId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    Assert.IsTrue(count > 0, $"Sensor with ID {sensorId} not found.");
                }
            }
        }

        [TestMethod()]
        public void UpdateSensorTest()
        {
            // Arrange
            var sensorDB = new SensorDB();
            int sensorId = 4; // Replace with a valid sensor ID
            double? newTemperatureValue = 25.0;
            double? newSoundValue = 35.0;

            // Act
            sensorDB.UpdateSensor(sensorId, newTemperatureValue, newSoundValue);

            // Assert: Check that the sensor was updated
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT TemperatureValue, SoundValue FROM Sensors WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sensorId);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Assert.AreEqual(newTemperatureValue, reader["TemperatureValue"]);
                            Assert.AreEqual(newSoundValue, reader["SoundValue"]);
                        }
                        else
                        {
                            Assert.Fail($"Sensor with ID {sensorId} not found.");
                        }
                    }
                }
            }
        }

        [TestMethod()]
        public void DeleteSensorTest()
        {
            // Arrange
            var sensorDB = new SensorDB();
            int sensorId = 1; // Replace with a valid sensor ID

            // Act
            sensorDB.DeleteSensor(sensorId);

            // Assert: Check that the sensor was deleted
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT COUNT(*) FROM Sensors WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sensorId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    Assert.AreEqual(0, count, $"Sensor with ID {sensorId} was not deleted.");
                }
            }
        }
    }
}
