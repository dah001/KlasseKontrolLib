public class TemperatureSensor : Sensor
{
    public double TemperatureValue { get; set; }
    public DateTime LastMeasurement { get; set; }

    public TemperatureSensor(int id, string sensorType, double temperatureValue, DateTime lastMeasurement)
        : base(id, sensorType, lastMeasurement)
    {
        TemperatureValue = temperatureValue;
    }
    
    public override string ToString()
    {
        return $"{{{nameof(Id)}={Id.ToString()}, {nameof(SensorType)}={SensorType}, {nameof(TemperatureValue)}={TemperatureValue.ToString()}, {nameof(LastMeasurement)}={LastMeasurement.ToString()}}}";
    }
}