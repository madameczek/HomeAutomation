using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using CommonClasses;
using TemperatureSensor.Models;

namespace TemperatureSensor
{
    public class TemperatureSensorService : BaseActor
    {
        public override string HwSettingsActorSection { get; } = "TemperatureSensor";
        HwSettings hwSettings = new HwSettings();
        private string DataPairsKeyTemperature { get; } = "temperature";

        private IConfiguration configuration;
        public TemperatureSensorService(IConfiguration configuration) 
        {
            this.configuration = configuration;
            DataPairs = new Dictionary<string, string>() { { DataPairsKeyTemperature, "0.0" } };
        }

        /*public Guid Guid { get; set; }
        public Guid DeviceGuid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Enum Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Data { get; set; }
        public IDictionary<string, string> DataPairs { get; set; }*/

        public override IMessage GetMessage()
        {
            IMessage message = new TemperatureSensorMessageModel();
            message.MessageTime = DateTimeOffset.Now;
            message.DataPairs = new Dictionary<string, string>() { { DataPairsKeyTemperature, DataPairs[DataPairsKeyTemperature] } };
            return message;
        }

        public override IActor ReadConfig()
        {
            throw new NotImplementedException();
        }

        public override async Task ConfigureService(CancellationToken ct = default)
        {
            try
            {
                if(!ct.IsCancellationRequested)
                {
                    hwSettings = configuration.GetSection(HwSettingsSection).GetSection(HwSettingsActorSection).Get<HwSettings>();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
            await Task.CompletedTask;
        }

        public override bool Write(IMessage message)
        {
            return true;
        }

        private async Task ReadTemp(CancellationToken ct = default)
        {
            try
            {
                string data = await Task.Run(() =>
                    File.ReadAllText(hwSettings.BasePath + hwSettings.HWSerial + @"/temperature"), ct);
                string _temperature = (int.Parse(data.Trim()) * 0.001).ToString("F1").Substring(0, 4);
                DataPairs[DataPairsKeyTemperature] = _temperature;
            }
            catch (OperationCanceledException) { }
            catch (Exception) { throw; }
        }

        public override async Task Run(CancellationToken ct = default)
        {
            // metoda periodycznie odczytuje DS1820. Efekt:
            // - zapisuje wynik do DataPairs "temperature", <string>
            // - kolejna wersja może porównywać odczyt z parametrem konfiguracyjnym uppperlimit i lowerlimit (+histereza)
            // i wywoływać event
            // - kolejna wersja może wykrywać anomalie (np. obniżenie temp o 5 st w czasie 5 minut i też wywoływać alarm

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await ReadTemp(ct);
                    Console.WriteLine(DataPairs[DataPairsKeyTemperature]);
                    await Task.Delay(hwSettings.ReadInterval, ct);
                }
            }
            catch(OperationCanceledException) { }
            catch(Exception) { throw; }
        }

        public override IActor Read()
        {
            throw new NotImplementedException();
        }
    }
}
