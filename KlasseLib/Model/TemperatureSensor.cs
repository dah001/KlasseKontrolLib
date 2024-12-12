namespace KlasseLib
{
    public class TemperatureSensor : Sensor
    {
        // Constructor for TemperatureSensor
        public TemperatureSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
            : base(id, sensorType, currentValue, lastMeasurement)
        {
            // Additional initialization specific to TemperatureSensor can go here
        }

        // Override ToString for more descriptive output if needed
        public override string ToString()
        {
            return $"TemperatureSensor [Id={Id}, Type={SensorType}, Value={CurrentValue}, Time={LastMeasurement:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}