﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using Shared.Models;

namespace Relay.Interfaces
{
    public interface IRelayService : IService
    {
        public new IEnumerable<IHwSettings> GetSettings();
        public Task ConfigureService(IHwSettings settings, CancellationToken ct);
        public Task Run(CancellationToken ct);
    }
}
