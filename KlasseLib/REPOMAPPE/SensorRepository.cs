namespace KlasseLib.REPOMAPPE;

public class SensorRepository : ISensorRepository
{
    private readonly List<Sensor> _sensors = new List<Sensor>();
    
    public void AddSensor(Sensor sensor)
    {
        if (sensor == null)
        {
            throw new ArgumentNullException(nameof(sensor));
        }

        _sensors.Add(sensor);
        Console.WriteLine($"Sensor with ID {sensor.Id} added to the repository");
    }

    public Sensor GetSensorById(int id)
    {
        foreach (var sensor in _sensors)
        {
            if (sensor.Id == id)
                return sensor;
        }
        return null;
    }
    
    public Sensor UpdateSensor(int id, double newValue)
    {
        Sensor sensor = GetSensorById(id);
        if (sensor == null)
        {
            throw new ArgumentNullException(nameof(sensor));
        }
        sensor.CurrentValue = newValue;
        sensor.LastMeasurement = DateTime.Now;
        return sensor;
    }
    
    public Sensor DeleteSensor (int id)
    {
        Sensor sensor = GetSensorById(id);
        if (sensor == null)
        {
            throw new KeyNotFoundException(nameof(sensor));
        }
        _sensors.Remove(sensor);
        return sensor;
    }
  
}