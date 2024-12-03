namespace KlasseLib;

public class MotionSensor: Sensor 
{
    public MotionSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement) : base(id, sensorType, currentValue, lastMeasurement)
    {
        
    }
}