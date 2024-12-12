using System;

namespace KlasseLib
{
    public abstract class Sensor
    {
        private double _currentValue;

        public int Id { get; set; }

        public string SensorType { get; set; }
        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(CurrentValue), "Value cannot be negative.");
                }
                _currentValue = value;
            }
        }
        public DateTime LastMeasurement { get; set; }

        protected Sensor(int id, string sensorType, double currentValue, DateTime lastMeasurement)
        {
            if (string.IsNullOrWhiteSpace(sensorType))
            {
                throw new ArgumentException("Sensor type cannot be null or empty.", nameof(sensorType));
            }

            Id = id;
            SensorType = sensorType;
            CurrentValue = currentValue;
            LastMeasurement = lastMeasurement;
        }

        public override string ToString()
        {
            return $"Sensor [Id={Id}, Type={SensorType}, Value={CurrentValue}, Time={LastMeasurement:yyyy-MM-dd HH:mm:ss}]";
        }
    }
}