using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KlasseLib.KlasseKontrolServices;

namespace KlasseWebService.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorsController : ControllerBase
    {
        private readonly SensorDB _sensorDb = new SensorDB(); // Database access layer

        // GET: api/sensors
        [HttpGet]
        public async Task<IActionResult> GetAllSensors()
        {
            try
            {
                // Hent alle sensorer fra databasen
                var sensors = await _sensorDb.GetAllSensorsAsync();

                if (sensors == null || sensors.Count == 0)
                {
                    return NotFound("No sensor data available.");
                }

                // Returnér sensordata som JSON
                return Ok(sensors);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllSensors: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving sensor data.");
            }
        }
    }
}