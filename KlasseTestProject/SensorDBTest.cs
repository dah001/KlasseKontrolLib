using System;
using System.Data.SqlClient;

using KlasseLib.KlasseKontrolRepository;

namespace KlasseTestProject.Services
{
    [TestClass]
    public class SensorDBIntegrationTests
    {
        private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test_thread;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private SensorDB _sensorDB;

        [TestInitialize]
        public void Setup()
        {

            
            // Opret testtabellen, hvis den ikke findes
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                    IF OBJECT_ID('dbo.Sensors', 'U') IS NULL
                    BEGIN
                        CREATE TABLE Sensors (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            SensorType NVARCHAR(50),
                            CurrentValue FLOAT,
                            LastMeasurement DATETIME
                        )
                    END", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Tøm tabellen efter hver test
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("TRUNCATE TABLE Sensors;", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void AddSensor_ShouldInsertSensorIntoDatabase()
        {
            // Arrange
            var sensor = new Sensor
            {
                SensorType = "Temperature",
                CurrentValue = 22.5,
                LastMeasurement = DateTime.Now
            };

            // Act
            _sensorDB.AddSensor(sensor);

            // Assert
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM Sensors WHERE SensorType = @SensorType", connection);
                command.Parameters.AddWithValue("@SensorType", sensor.SensorType);

                var count = (int)command.ExecuteScalar();
                Assert.AreEqual(1, count, "Sensor blev ikke korrekt indsat i databasen.");
            }
        }

        [TestMethod]
        public void GetSensorById_ShouldReturnCorrectSensor()
        {
            // Arrange
            var sensor = new Sensor
            {
                SensorType = "Humidity",
                CurrentValue = 45.3,
                LastMeasurement = DateTime.Now
            };

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Sensors (SensorType, CurrentValue, LastMeasurement) 
                    VALUES (@SensorType, @CurrentValue, @LastMeasurement);
                    SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@SensorType", sensor.SensorType);
                command.Parameters.AddWithValue("@CurrentValue", sensor.CurrentValue);
                command.Parameters.AddWithValue("@LastMeasurement", sensor.LastMeasurement);

                sensor.Id = Convert.ToInt32(command.ExecuteScalar());
            }

            // Act
            var result = _sensorDB.GetSensorById(sensor.Id);

            // Assert
            Assert.IsNotNull(result, "Sensor blev ikke fundet.");
            Assert.AreEqual(sensor.SensorType, result.SensorType, "SensorType matcher ikke.");
            Assert.AreEqual(sensor.CurrentValue, result.CurrentValue, "CurrentValue matcher ikke.");
        }

        [TestMethod]
        public void DeleteSensor_ShouldRemoveSensorFromDatabase()
        {
            // Arrange
            var sensor = new Sensor
            {
                SensorType = "Pressure",
                CurrentValue = 1013.2,
                LastMeasurement = DateTime.Now
            };

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                    INSERT INTO Sensors (SensorType, CurrentValue, LastMeasurement) 
                    VALUES (@SensorType, @CurrentValue, @LastMeasurement);
                    SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@SensorType", sensor.SensorType);
                command.Parameters.AddWithValue("@CurrentValue", sensor.CurrentValue);
                command.Parameters.AddWithValue("@LastMeasurement", sensor.LastMeasurement);

                sensor.Id = Convert.ToInt32(command.ExecuteScalar());
            }

            // Act
            var deletedSensor = _sensorDB.DeleteSensor(sensor.Id);

            // Assert
            Assert.IsNotNull(deletedSensor, "Slettet sensor er null.");
            Assert.AreEqual(sensor.Id, deletedSensor.Id, "Slettet sensor-ID matcher ikke.");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM Sensors WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", sensor.Id);

                var count = (int)command.ExecuteScalar();
                Assert.AreEqual(0, count, "Sensor blev ikke slettet.");
            }
        }
    }
}
