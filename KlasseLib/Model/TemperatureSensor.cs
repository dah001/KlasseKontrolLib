namespace KlasseLib
{
    public class TemperatureSensor : Sensor
    {

        public TemperatureSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
            : base(id, sensorType, currentValue, lastMeasurement)
        {
      
        }

  
        public override string ToString()
        {
            return $"TemperatureSensor [Id={Id}, Type={SensorType}, Value={CurrentValue}, Time={LastMeasurement:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}