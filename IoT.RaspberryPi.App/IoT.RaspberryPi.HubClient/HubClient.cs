using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace IoT.RaspberryPi.HubClient
{
    public class HubClient : IHubClient
    {
        private readonly DeviceClient _deviceClient;

        public HubClient(string deviceConnectionString)
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString, TransportType.Mqtt);
        }

        public async Task SendMessage(SensorReading sensorReading)
        {
            string jsonMessage = JsonConvert.SerializeObject(sensorReading);
            Message message = new Message(Encoding.ASCII.GetBytes(jsonMessage));

            await _deviceClient.SendEventAsync(message);
        }
    }
}
