using System.Threading.Tasks;

namespace IoT.RaspberryPi.HubClient
{
    public interface IHubClient
    {
        Task SendMessage(SensorReading sensorReading);
    }
}