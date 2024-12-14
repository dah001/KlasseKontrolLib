using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.KlasseKontrolServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlasseLib.KlasseKontrolServices.Tests
{
    [TestClass]
    public class SensorDBTests
    {
        private readonly SensorDB _sensorDb = new SensorDB();

        [TestMethod]
        public async Task GetAllSensorsTest()
        {
            // Act
            List<Sensor> sensors = await _sensorDb.GetAllSensorsAsync();

            // Assert
            Assert.IsNotNull(sensors, "Sensor data retrieval failed.");
            Assert.IsTrue(sensors.Count > 0, "No sensor data found.");
        }
    }
}