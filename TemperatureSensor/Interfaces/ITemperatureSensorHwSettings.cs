using Shared.Models;

namespace TemperatureSensor.Models
{
    public interface ITemperatureSensorHwSettings : IHwSettings
    {
        string BasePath { get; set; }

        /// <summary>
        /// Interval between subsequent inserts of temperature sensor messages into a local database.
        /// Expressed in minutes.
        /// </summary>
        int DatabasePushPeriod { get; set; }
        string HwSerial { get; set; }
    }
}