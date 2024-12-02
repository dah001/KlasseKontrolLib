using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasseLib
{
    public class Sensor
    {
        public int Id { get; }
        public string SensorType { get; }
        public double CurrentValue { get; }
        public DateTime LastMeasurement { get; }

        public Sensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
        {
            Id = id;
            SensorType = sensorType;
            CurrentValue = currentValue;
            LastMeasurement = lastMeasurement;
        }
    }
}
