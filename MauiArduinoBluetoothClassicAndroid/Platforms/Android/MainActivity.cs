using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using MauiArduinoBluetoothClassicAndroid.Bluetooth;
using MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth;

namespace MauiArduinoBluetoothClassicAndroid;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : MauiAppCompatActivity
{
    const int RequestBluetoothPermissionsId = 1001;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
		Platform.Init(this, savedInstanceState);
    }
}
