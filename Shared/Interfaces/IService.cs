using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public interface IService
    {
        public string HwSettingsSection { get; }
        public string HwSettingsCurrentActorSection { get; }

        /// <summary>
        /// This method is responsible for fetching settings from IConfiguration object.
        /// It must be called before Task ConfigureService(CancellationToken ct)
        /// </summary>
        /// <returns></returns>
        public IHwSettings GetSettings();

        public Task ConfigureService(CancellationToken ct);

        /// <summary>
        /// Method is invoked periodically by Launcher object. 
        /// The method reads data from physical device.        
        /// Data are stored in a private object inside service object, which invoked the method.
        /// Therefore this method has no result type.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ReadDeviceAsync(CancellationToken ct);

        /// <summary>
        /// Creates a message from data held by a service object. 
        /// It is used internally by the service.
        /// </summary>
        /// <returns></returns>
        public IMessage GetMessage();

        /// <summary>
        /// Request queue to save message to a storage defined in the queue.
        /// Usually it requests message by invoke of GetMessage() and adds it do a queue.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SaveMessageAsync(CancellationToken ct);
    }
}
