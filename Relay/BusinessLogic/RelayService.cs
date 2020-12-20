using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared.Models;

namespace Relay
{
    internal class RelayService : IRelayService
    {
        public string HwSettingsSection { get; }
        public string HwSettingsCurrentActorSection { get; }
        public IHwSettings GetSettings()
        {
            throw new NotImplementedException();
        }

        public Task ConfigureService(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task ReadDeviceAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public IMessage GetMessage()
        {
            throw new NotImplementedException();
        }

        public Task SaveMessageAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
