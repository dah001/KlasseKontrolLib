using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using KlasseLib;
using KlasseLib.KlasseKontrolServices;

namespace KlasseWebService.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorDB _sensorDB;

        public SensorsController(ISensorDB sensorDB)
        {
            _sensorDB = sensorDB;
        }

        // GET: api/sensors
        [HttpGet]
        public IActionResult GetAllSensors()
        {
            try
            {
                // Hent alle sensorer fra databasen via ISensorDB
                var sensors = _sensorDB.GetAllSensors();

                if (sensors == null || !sensors.Any())
                {
                    return NotFound("No sensors found in the database.");
                }

                return Ok(sensors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // GET: api/sensors/{id}
        [HttpGet("{id}")]
        public IActionResult GetSensorById(int id)
        {
            try
            {
                var sensor = _sensorDB.GetSensorById(id); // Hent sensor fra databasen
                if (sensor == null)
                {
                    return NotFound(new { Message = $"Sensor with ID {id} not found." });
                }

                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        // POST: api/sensors
        [HttpPost("db")]
        public IActionResult AddSensorToDB([FromBody] Sensor sensor)
        {
            try
            {
                if (sensor is TemperatureSensor tempSensor)
                {
                    // Tilføj temperaturværdi til databasen
                    _sensorDB.AddSensor(
                        sensorType: tempSensor.SensorType,
                        temperatureValue: tempSensor.TemperatureValue,
                        soundValue: null,
                        lastMeasurement: tempSensor.LastMeasurement
                    );
                }
                else if (sensor is SoundSensor soundSensor)
                {
                    // Tilføj lydværdi til databasen
                    _sensorDB.AddSensor(
                        sensorType: soundSensor.SensorType,
                        temperatureValue: null,
                        soundValue: soundSensor.SoundValue,
                        lastMeasurement: soundSensor.LastMeasurement
                    );
                }
                else
                {
                    return BadRequest("Unknown sensor type.");
                }

                return Ok(new { Message = "Sensor added to database." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
