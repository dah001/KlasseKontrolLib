using KlasseLib;

namespace KlasseWebService.Model
{
    public class SensorConverter
    {
    
        // Convert SensorDTO to Sensor
        public static Sensor ConvertToSensor(SensorDTO sensorDTO)
        {
            if (sensorDTO == null)
            {
                return null; // Return null if sensorDTO is null, or throw exception if needed
            }

            // Validate that the sensor type is correct before creating the sensor
            if (string.IsNullOrWhiteSpace(sensorDTO.SensorType))
            {
                throw new InvalidOperationException("Sensor type must be provided.");
            }

            // Handle invalid or inactive sensors based on the DTO data
            if (sensorDTO.SensorType != "Temperature" && sensorDTO.SensorType != "Sound")
            {
                throw new InvalidOperationException("Unknown or inactive sensor type");
            }

            // Create the correct sensor type based on SensorType in DTO
            Sensor sensor = sensorDTO.SensorType switch
            {
                "Temperature" => new TemperatureSensor(
                    sensorDTO.Id, 
                    sensorDTO.SensorType, 
                    sensorDTO.TemperatureValue ?? 0, 
                    sensorDTO.LastMeasurement),
                "Sound" => new SoundSensor(
                    sensorDTO.Id, 
                    sensorDTO.SensorType, 
                    sensorDTO.SoundValue ?? 0, 
                    sensorDTO.LastMeasurement),
                _ => throw new InvalidOperationException("Unknown sensor type") // Handle any other unexpected sensor types
            };

            return sensor;
        }

        // Convert Sensor to SensorDTO
        public static SensorDTO ConvertToDTO(Sensor sensor)
        {
            if (sensor == null)
            {
                return null; // Return null if sensor is null, or throw exception if needed
            }

            // Map properties from Sensor to SensorDTO
            var sensorDTO = new SensorDTO
            {
                Id = sensor.Id,
                SensorType = sensor.SensorType,
                LastMeasurement = sensor.LastMeasurement
            };

            // Add specific values for Temperature or Sound sensor
            if (sensor is TemperatureSensor temperatureSensor)
            {
                sensorDTO.TemperatureValue = temperatureSensor.CurrentValue;
            }
            else if (sensor is SoundSensor soundSensor)
            {
                sensorDTO.SoundValue = soundSensor.CurrentValue;
            }

            return sensorDTO;
        }
    }
}