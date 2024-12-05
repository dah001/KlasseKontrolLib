using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using KlasseLib;

namespace KlasseWebService.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorsController : ControllerBase
    {
        // Simulerer en in-memory database
        private static List<Sensor> _sensors = new List<Sensor>
        {
            new TemperatureSensor(1, "Temperature", 23.5, DateTime.Now.AddMinutes(-10)),
            new SoundSensor(2, "Sound", 75.3, DateTime.Now.AddMinutes(-5))
        };

        // GET: api/sensors
        [HttpGet]
        public IActionResult GetAllSensors()
        {
            return Ok(_sensors);
        }

        // GET: api/sensors/{id}
        [HttpGet("{id}")]
        public IActionResult GetSensorById(int id)
        {
            var sensor = _sensors.FirstOrDefault(s => s.Id == id);
            if (sensor == null)
                return NotFound($"Sensor with ID {id} not found.");
            return Ok(sensor);
        }

        // POST: api/sensors
        [HttpPost]
        public IActionResult AddSensor([FromBody] Sensor sensor)
        {
            if (sensor == null)
                return BadRequest("Sensor data is invalid.");

            // Assign a new ID to the sensor
            sensor.Id = _sensors.Any() ? _sensors.Max(s => s.Id) + 1 : 1;
            _sensors.Add(sensor);
            return CreatedAtAction(nameof(GetSensorById), new { id = sensor.Id }, sensor);
        }

        // PUT: api/sensors/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateSensor(int id, [FromBody] Sensor updatedSensor)
        {
            var sensor = _sensors.FirstOrDefault(s => s.Id == id);
            if (sensor == null)
                return NotFound($"Sensor with ID {id} not found.");

            sensor.SensorType = updatedSensor.SensorType;
            sensor.CurrentValue = updatedSensor.CurrentValue;
            sensor.LastMeasurement = updatedSensor.LastMeasurement;

            return Ok(sensor);
        }

        // DELETE: api/sensors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteSensor(int id)
        {
            var sensor = _sensors.FirstOrDefault(s => s.Id == id);
            if (sensor == null)
                return NotFound($"Sensor with ID {id} not found.");

            _sensors.Remove(sensor);
            return Ok($"Sensor with ID {id} deleted successfully.");
        }
    }
}
