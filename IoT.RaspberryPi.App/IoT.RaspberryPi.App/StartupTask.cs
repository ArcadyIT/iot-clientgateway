using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using GrovePi;
using GrovePi.I2CDevices;
using GrovePi.Sensors;
using IoT.RaspberryPi.HubClient;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace IoT.RaspberryPi.App
{
    public sealed class StartupTask : IBackgroundTask
    {
        private readonly IDHTTemperatureAndHumiditySensor _groveTempHumi = DeviceFactory.Build.DHTTemperatureAndHumiditySensor(Pin.DigitalPin3, DHTModel.Dht11);
        private readonly IRgbLcdDisplay _display = DeviceFactory.Build.RgbLcdDisplay();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            string connectionString = new ConnectionStringBuilder().Build();

            IHubClient hubClient = new HubClient.HubClient(connectionString);

            _display.SetBacklightRgb(255, 0, 255);

            double previousTemperatureInCelsius = 0;
            double previousHumidity = 0;

            while (true)
            {
                _groveTempHumi.Measure();
                double temperatureInCelsius = _groveTempHumi.TemperatureInCelsius;
                double humidity = _groveTempHumi.Humidity;

                if (!temperatureInCelsius.Equals(previousTemperatureInCelsius))
                {
                    hubClient.SendMessage(new SensorReading
                    {
                        Type = SensorDataTypeEnum.TemperatureCelsius,
                        Value = temperatureInCelsius.ToString(CultureInfo.InvariantCulture),
                    });
                    Debug.WriteLine("Temperature changed: Sending message");
                }
                previousTemperatureInCelsius = temperatureInCelsius;

                if (!humidity.Equals(previousHumidity))
                {
                    hubClient.SendMessage(new SensorReading
                    {
                        Type = SensorDataTypeEnum.Humidity,
                        Value = humidity.ToString(CultureInfo.InvariantCulture)
                    });
                    Debug.WriteLine("Humidity changed: Sending message");
                }
                previousHumidity = humidity;

                string displayValue = $"Temp: {temperatureInCelsius}c       Humidity: {humidity}% ";
                _display.SetText(displayValue);
                Debug.WriteLine(displayValue);
                Task.Delay(500).Wait();
            }
        }
    }
}
