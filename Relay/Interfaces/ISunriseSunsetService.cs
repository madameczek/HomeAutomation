using System;
using System.Collections.Generic;
using System.Text;
using Shared;
using static Relay.SunriseSunsetService;

namespace Relay.Interfaces
{
    public interface ISunriseSunsetService : IService
    {
        public event SunsetEventHandler Sunset;
    }
}
