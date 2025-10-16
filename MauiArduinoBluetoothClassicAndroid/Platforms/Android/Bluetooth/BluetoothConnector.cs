using Android.Bluetooth;
using Java.Util;
using MauiArduinoBluetoothClassicAndroid.Bluetooth;
using MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth;
using System.Text;

[assembly: Dependency(typeof(BluetoothConnector))]
namespace MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth
{
    public class BluetoothConnector : IBluetoothConnector
    {
        private BluetoothSocket _socket; 
        /// <inheritdoc />
        public async Task<List<string>> GetConnectedDevices()
        {
            PermissionStatus status = await RequestBluetoothConnectPermission();
            if (status == PermissionStatus.Granted)
            {
                _adapter = BluetoothAdapter.DefaultAdapter;
                if (_adapter == null)
                    throw new Exception("No Bluetooth adapter found.");

                if (_adapter.IsEnabled)
                {
                    if (_adapter.BondedDevices.Count > 0)
                    {
                        return _adapter.BondedDevices.Select(d => d.Name).ToList();
                    }
                }
                else
                {
                    Console.Write("Bluetooth is not enabled on device");
                }
                return new List<string>();
            }
            return null;
        }

        /// <inheritdoc />
        public async void Connect(string deviceName, string data)
        {
            PermissionStatus status = await RequestBluetoothConnectPermission();
            if (status == PermissionStatus.Granted)
            {
                var device = _adapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);
                if (_socket == null || !_socket.IsConnected)
                {
                    _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString(SspUdid));
                    try
                    {
                        _socket.Connect();
                    }
                    catch(Exception ex)
                    {
                        var msg = ex.Message;
                    }
                    var buffer = StringToByteArray(data, Encoding.UTF8);

                    // Write data to the device to trigger LED
                    _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                }
                else
                {
                    var buffer = StringToByteArray(data, Encoding.UTF8);

                    // Write data to the device to trigger LED
                    _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                }

            }
        }

        public byte[] StringToByteArray(string text, Encoding encoding)
        {
            // 1. Utilisez la méthode GetBytes() de l'encodage.
            byte[] bytes = encoding.GetBytes(text);

            return bytes;
        }

        public async Task<PermissionStatus> RequestBluetoothConnectPermission()
        {
            // C'est l'approche MAUI standard, qui peut englober les permissions Android 12+ dans les versions récentes
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Bluetooth>();
            }

            // Si la permission 'Bluetooth' n'est pas disponible, vous devrez peut-être en créer une personnalisée.
            // L'exemple ci-dessous est une classe personnalisée (voir Option B).
            // PermissionStatus status = await Permissions.RequestAsync<MyBluetoothConnectPermission>(); 

            return status;
        }

        /// <summary>
        /// The standard UDID for SSP
        /// </summary>
        private const string SspUdid = "00001101-0000-1000-8000-00805f9b34fb";
        private BluetoothAdapter _adapter;
    }
}