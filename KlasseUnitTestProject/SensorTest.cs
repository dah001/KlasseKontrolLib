using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class SensorTest
{
    [TestMethod]
    public void Constructor_ShouldSetValuesCorrectly()
    {
        // Arrange
        var expectedId = 1;
        var expectedSensorType = "Temperature";
        var expectedCurrentValue = 22.5;
        var expectedLastMeasurement = DateTime.Now;

        // Act
        var sensor = new Sensor(expectedId, expectedSensorType, expectedCurrentValue, expectedLastMeasurement);

        // Assert
        Assert.AreEqual(expectedId, sensor.Id);
        Assert.AreEqual(expectedSensorType, sensor.SensorType);
        Assert.AreEqual(expectedCurrentValue, sensor.CurrentValue);
        Assert.AreEqual(expectedLastMeasurement, sensor.LastMeasurement);
    }
}

