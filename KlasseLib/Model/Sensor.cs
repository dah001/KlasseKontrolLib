public abstract class Sensor
{
    public int Id { get; set; }
    public string SensorType { get; set; }
    public DateTime LastMeasurement { get; set; }

    // Parameterløs konstruktor for JSON deserialisering
    public Sensor() { }

    // Også din eksisterende parameteriserede konstruktor
    public Sensor(int id, string sensorType, DateTime lastMeasurement)
    {
        Id = id;
        SensorType = sensorType;
        LastMeasurement = lastMeasurement;
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(SensorType)}: {SensorType}, {nameof(LastMeasurement)}: {LastMeasurement}";
    }
}