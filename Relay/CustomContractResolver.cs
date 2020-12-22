using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json.Serialization;

namespace Relay
{
    internal class CustomContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMapping { get; set; }

        public CustomContractResolver(Dictionary<string, string> dataFielsNames)
        {
            this.PropertyMapping = new Dictionary<string, string>()
            {
                {"Sunrise", dataFielsNames["Sunrise"]},
                {"Sunset", dataFielsNames["Sunset"]},
                {"SolarNoon", dataFielsNames["SolarNoon"]},
                {"DayLengthSeconds", dataFielsNames["DayLengthSeconds"]},
                {"CivilTwilightBegin", dataFielsNames["CivilTwilightBegin"]},
                {"NauticalTwilightBegin", dataFielsNames["NauticalTwilightBegin"]},
                {"NauticalTwilightEnd", dataFielsNames["NauticalTwilightEnd"]},
                {"AstronomicalTwilightBegin", dataFielsNames["AstronomicalTwilightBegin"]},
                {"AstronomicalTwilightEnd", dataFielsNames["AstronomicalTwilightEnd"]},
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = this.PropertyMapping.TryGetValue(propertyName, out var resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
