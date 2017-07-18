using System;

namespace IoT.RaspberryPi.HubClient
{
    public sealed class SensorReading
    {
        public SensorReading()
        {
            TimeStamp = DateTime.Now;
        }

        public SensorDataTypeEnum Type { get; set; }

        public string Value { get; set; }

        public DateTime TimeStamp { get; }
    }
}