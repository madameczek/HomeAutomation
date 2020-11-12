using Shared.Models;

namespace TemperatureSensor.Models
{
    public interface ITemperatureSensorHwSettings : IHwSettings
    {
        string BasePath { get; set; }

        /// <summary>
        /// Interval between subsequent inserts of temperature sensor messages into a local database.
        /// Expressed in seconds. If can't be read from config file, default value is applied.
        /// Default value is set in service's launcher object for 1200 s (5 min).
        /// </summary>
        int DatabasePushPeriod { get; set; }
        string HWSerial { get; set; }
    }
}