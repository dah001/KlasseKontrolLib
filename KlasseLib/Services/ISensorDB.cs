using System;

namespace KlasseLib.KlasseKontrolServices
{
    public interface ISensorDB
    {
        // Add a sensor to the database
        void AddSensor(string sensorType, double? temperatureValue, double? soundValue, DateTime lastMeasurement);

        // Retrieve a sensor by ID from the database
        void GetSensorById(int id);

        // Update a sensor's values and last measurement in the database
        void UpdateSensor(int id, double? newTemperatureValue, double? newSoundValue);

        // Delete a sensor by ID from the database
        void DeleteSensor(int id);
    }
}
