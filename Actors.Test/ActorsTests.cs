using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace CommonClasses.Test
{
    public class ActorsTests
    {
/*        [Fact]
        public void Serialize()
        {
            // Arrange
            BaseDevice device = new BaseDevice
            {
                DataPairs = new Dictionary<string, string>() {
                { "temperature", "20.0" },
                { "message", "this is g¹bka" } }
            };
            // Act
            _ = device.Serialize();
            // Assert
            Assert.Equal("{\"temperature\":\"20.0\",\"message\":\"this is g¹bka\"}", device.Data);
        }

        [Fact]
        public void Deserialize()
        {
            // Arrange
            BaseDevice device = new BaseDevice
            {
                Data = "{\"temperature\":\"20.0\",\"message\":\"this is g¹bka\"}"
            };
            BaseDevice deviceToCompare = new BaseDevice();
            deviceToCompare.DataPairs = new Dictionary<string, string>() {
                { "temperature", "20.0" },
                { "message", "this is g¹bka" } };
            // Act
            _ = device.Deserialize();
            // Assert
            Assert.Equal(deviceToCompare.DataPairs, device.DataPairs);
        }*/
    }
}
