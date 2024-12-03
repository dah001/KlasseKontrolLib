using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasseLib
{
    public abstract class Sensor
    {
        public int Id { get; set; }
        public string SensorType { get; set; }
        public double CurrentValue { get; set; }
        public DateTime LastMeasurement { get; set; }

        public Sensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
        {
            Id = id;
            SensorType = sensorType;
            CurrentValue = currentValue;
            LastMeasurement = lastMeasurement;
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(SensorType)}={SensorType}, {nameof(CurrentValue)}={CurrentValue.ToString()}, {nameof(LastMeasurement)}={LastMeasurement.ToString()}}}";
        }
    }
}
