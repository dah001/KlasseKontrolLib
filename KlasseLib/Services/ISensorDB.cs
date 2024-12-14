using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlasseLib.KlasseKontrolServices
{
    public interface ISensorDB
    {
        // Retrieve all sensor data from both SoundSensor and TemperatureSensor tables
        Task<List<Sensor>> GetAllSensorsAsync();
    }
}