using KlasseLib;

namespace KlasseTestProject
{

    [TestClass]
    public class SensorTest
    {
        // Test method for the Temperature sensor
        [TestMethod]
        public void Constructor_ShouldSetTemperatureValuesCorrectly()
        {
            // Arrange
            var expectedId = 1;
            var expectedSensorType = "Temperature";
            var expectedCurrentValue = 22.5;
            var expectedLastMeasurement = DateTime.Now;

            // Act
            var sensor = new TemperatureSensor(expectedId, expectedSensorType, expectedCurrentValue, expectedLastMeasurement);

            // Assert
            Assert.AreEqual(expectedId, sensor.Id);
            Assert.AreEqual(expectedSensorType, sensor.SensorType);
            Assert.AreEqual(expectedCurrentValue, sensor.CurrentValue);
            Assert.AreEqual(expectedLastMeasurement, sensor.LastMeasurement);
        }
    
        // Test method for the Sound sensor
        [TestMethod]
        public void Constructor_ShouldSetSoundValuesCorrectly()
        {
            // Arrange
            var expectedId = 1; 
            var expectedSensorType = "Sound";
            var expectedCurrentValue = 75.0;
            var expectedLastMeasurement = DateTime.Now;

            // Act
            var sensor = new SoundSensor(expectedId, expectedSensorType, expectedCurrentValue, expectedLastMeasurement);

            // Assert
            Assert.AreEqual(expectedId, sensor.Id);
            Assert.AreEqual(expectedSensorType, sensor.SensorType);
            Assert.AreEqual(expectedCurrentValue, sensor.CurrentValue);
            Assert.AreEqual(expectedLastMeasurement, sensor.LastMeasurement);
        }
    }
    
}