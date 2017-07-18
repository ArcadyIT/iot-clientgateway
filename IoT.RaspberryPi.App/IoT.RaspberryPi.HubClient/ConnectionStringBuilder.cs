using System.IO;
using Newtonsoft.Json;

namespace IoT.RaspberryPi.HubClient
{
    public class ConnectionStringBuilder
    {
        public string Build()
        {
            string filePath = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "config.json");
            string configJson = File.ReadAllText(filePath);

            AppConfig config = JsonConvert.DeserializeObject<AppConfig>(configJson);

            return $"HostName={config.IoTHub.Hostname};DeviceId={config.IoTHub.DeviceId};SharedAccessKey={config.IoTHub.SharedAccessKey}"; 
        }
    }
}