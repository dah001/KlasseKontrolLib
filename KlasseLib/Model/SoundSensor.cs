namespace KlasseLib
{
    public class SoundSensor : Sensor
    {

        public SoundSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
            : base(id, sensorType, currentValue, lastMeasurement)
        {
        
        }

      
        public override string ToString()
        {
            return $"SoundSensor [Id={Id}, Type={SensorType}, Value={CurrentValue}, Time={LastMeasurement:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}