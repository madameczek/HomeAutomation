using System.Collections.Generic;

namespace GsmModem.Models
{
    internal class GsmModemCommands
    {
        public string AccountBalanceQuery { get; set; }
        public string FlightModeOn { get; set; }
        public string FlightModeOff { get; set; }
/*        public string Init1Echo { get; set; }
        public string Init2InterfaceFlowControl { get; set; }
        public string Init3SmsMode { get; set; }
        public string Init4DtmfConfig { get; set; }
        public string Init5RingsBeforeAnswer { get; set; }
        public string Init6ClipConfig { get; set; }*/
        public Dictionary<string, string> InitCommands { get; set; }
    }
}
