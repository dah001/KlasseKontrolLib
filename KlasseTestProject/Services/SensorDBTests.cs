using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.KlasseKontrolServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlasseLib.KlasseKontrolServices.Tests
{
    /// <summary>
    /// Testklasse for at verificere funktionaliteten af SensorDB-klassen.
    /// </summary>
    [TestClass]
    public class SensorDBTests
    {
        /// <summary>
        /// SensorDB instans til testformål.
        /// </summary>
        private readonly SensorDB _sensorDb = new SensorDB();

        /// <summary>
        /// Tester metoden GetAllSensorsAsync for at sikre, at den henter alle sensorer korrekt.
        /// </summary>
        [TestMethod]
        public async Task GetAllSensorsTest()
        {
            // Act: Henter alle sensorer via SensorDB.
            List<Sensor> sensors = await _sensorDb.GetAllSensorsAsync();

            // Assert: Kontrollerer, at sensorerne ikke er null, og at der er mindst én sensor.
            Assert.IsNotNull(sensors, "Hentning af sensordata mislykkedes.");
            Assert.IsTrue(sensors.Count > 0, "Ingen sensordata fundet.");
        }
    }
}