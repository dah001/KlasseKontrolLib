namespace KlasseLib;

public class TemperatureSensor : Sensor 

{
    // Constructor for TemperatureSensor
    public TemperatureSensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
        : base(id, sensorType, currentValue, lastMeasurement) // Call the base class constructor
    {
        // You can add additional initialization specific to TemperatureSensor if needed
    }
}