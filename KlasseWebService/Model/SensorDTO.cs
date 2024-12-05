namespace KlasseWebService.Model;

public class SensorDTO
{
    public int Id { get; set; }
    public string? SensorType { get; set; }  // "Temperature" eller "Sound"
    public double? TemperatureValue { get; set; }
    public double? SoundValue { get; set; }
    public DateTime LastMeasurement { get; set; }
}