using System;
using JetBrains.Annotations;
using KlasseLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MotionSensorTest.Model;

[TestClass]
[TestSubject(typeof(MotionSensor))]
public class MotionSensorTest
{
    [TestMethod]
    public void Constructor_Shouldgetstudentcount()
    {
        //Arrange
        var expectedId = 2;
        var expectedSensorType = "motion";
        var expectedCurrentValue = 0.0;
        var expectedLastMeasurement = DateTime.Now;

        //Act
        var sensor = new MotionSensor(expectedId, expectedSensorType, expectedCurrentValue, expectedLastMeasurement);

        // Assert
        Assert.AreEqual(expectedId, sensor.Id, "Id should be correctly set by the constructor.");
        Assert.AreEqual(expectedSensorType, sensor.SensorType, "SensorType should be correctly set by the constructor.");
        Assert.AreEqual(expectedCurrentValue, sensor.CurrentValue, "CurrentValue should be correctly set by the constructor.");
        Assert.AreEqual(expectedLastMeasurement, sensor.LastMeasurement, "LastMeasurement should be correctly set by the constructor.");
    }
}