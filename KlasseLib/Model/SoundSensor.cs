namespace KlasseLib
{
    public class SoundSensor : Sensor
    {
        // Constructor for SoundSensor
        public SoundSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
            : base(id, sensorType, currentValue, lastMeasurement)
        {
            // Additional initialization specific to SoundSensor can go here
        }

        // Override ToString for more descriptive output if needed
        public override string ToString()
        {
            return $"SoundSensor [Id={Id}, Type={SensorType}, Value={CurrentValue}, Time={LastMeasurement:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}