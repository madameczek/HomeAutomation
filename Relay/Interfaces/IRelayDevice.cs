using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Relay.Models;

namespace Relay.Interfaces
{
    public interface IRelayDevice
    {
        public void Configure(RelayHwSettings settings);
        public Task SetOn();
        public Task SetOff();
        public Task Toggle();
    }
}
