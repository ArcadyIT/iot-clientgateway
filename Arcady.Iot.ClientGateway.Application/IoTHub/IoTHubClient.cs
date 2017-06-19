using Microsoft.Devices.Tpm;

namespace Arcady.IoT.ClientGateway.HubClient
{
    public class IoTHubClient : IIoTHubClient
    {
        private TpmDevice tpmDevice;

        public IoTHubClient(TpmDevice tpmDevice)
        {
            this.tpmDevice = tpmDevice;
        }
    }
}
