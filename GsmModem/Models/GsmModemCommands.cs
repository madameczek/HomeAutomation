using System.Collections.Generic;

namespace GsmModem.Models
{
    internal class GsmModemCommands
    {
        public string AccountBalanceQuery { get; set; }
        public string FlightModeOn { get; set; }
        public string FlightModeOff { get; set; }
        public Dictionary<string, string> InitCommands { get; }
    }
}
