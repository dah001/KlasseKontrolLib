using System;

namespace KlasseLib.KlasseKontrolServices
{
    public interface ISensorDB
    {
        Sensor GetSensorById(int id);
        void AddSensor(string sensorType, double? temperatureValue, double? soundValue, DateTime lastMeasurement);
        List<Sensor> GetAllSensors();
    }
}
