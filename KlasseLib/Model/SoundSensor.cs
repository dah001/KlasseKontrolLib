namespace KlasseLib;

public class SoundSensor : Sensor
{
    public double SoundValue { get; set; }
    public DateTime LastMeasurement { get; set; }

    public SoundSensor(int id, string sensorType, double soundValue, DateTime lastMeasurement)
        : base(id, sensorType, lastMeasurement)
    {
        SoundValue = soundValue;
    }

    public override string ToString()
    {
        return $"{{{nameof(Id)}={Id.ToString()}, {nameof(SensorType)}={SensorType}, {nameof(SoundValue)}={SoundValue.ToString()}, {nameof(LastMeasurement)}={LastMeasurement.ToString()}}}";
    }
}