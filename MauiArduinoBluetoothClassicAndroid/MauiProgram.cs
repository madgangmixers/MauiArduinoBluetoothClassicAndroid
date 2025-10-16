namespace MauiArduinoBluetoothClassicAndroid;

using MauiArduinoBluetoothClassicAndroid.Bluetooth;
#if ANDROID
using MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth;
using Android.Content;
using MauiArduinoBluetoothClassicAndroid.Platforms.Android;
#endif

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<MauiArduinoBluetoothClassicAndroid.App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

#if ANDROID
        builder.Services.AddSingleton<IBluetoothConnector, BluetoothConnector>();
		builder.Services.AddSingleton<MauiArduinoBluetoothClassicAndroid.IAudioPlayerService, MauiArduinoBluetoothClassicAndroid.Platforms.Android.AudioPlayerService>();
        builder.Services.AddSingleton<IBackgroundScheduler, AndroidScheduler>();
#endif
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
	}
}
