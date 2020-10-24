using CommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CommonClasses;
using GsmModem.Models;
using System.Linq;
using CommonClasses.Models;

namespace GsmModem
{
    public class GsmModemService : BaseService
    {
        public override string HwSettingsActorSection { get; } = "GsmModem";
        HwSettings hwSettings = new HwSettings();

        IConfiguration configuration;
        public GsmModemService(IConfiguration configuration)
        {
            this.configuration = configuration;
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
            throw new NotImplementedException();
        }

        public override IService Read()
        {
            throw new NotImplementedException();
        }

        public override IService ReadConfig()
        {
            throw new NotImplementedException();
        }

        public override Task Run(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public override async Task ConfigureService(CancellationToken ct = default)
        {
            // Select configs representing GSM modem
            try
            {
                if (!ct.IsCancellationRequested)
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
            throw new NotImplementedException();
        }
    }
}
