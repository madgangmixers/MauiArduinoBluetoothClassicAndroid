using Android.Media;
using Android.Content;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(MauiArduinoBluetoothClassicAndroid.Platforms.Android.AudioPlayerService))]
namespace MauiArduinoBluetoothClassicAndroid.Platforms.Android
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private MediaPlayer _player;
        public void PlayAudio(string fileName)
        {
            if (_player != null)
            {
                _player.Stop();
                _player.Release();
                _player = null;
            }

            try
            {
                // Convertir le nom du fichier (ex: "son.mp3") en identifiant de ressource Android
                // Les fichiers dans 'Resources/Raw' sont automatiquement convertis en minuscules
                // et l'extension n'est pas incluse dans l'identifiant.
                string resourceName = System.IO.Path.GetFileNameWithoutExtension(fileName).ToLowerInvariant();

                int resourceId = Platform.CurrentActivity.Resources.GetIdentifier(resourceName, "raw", Platform.CurrentActivity.PackageName);

                if (resourceId != 0)
                {
                    _player = MediaPlayer.Create(Platform.CurrentActivity, resourceId);
                    _player.Start();
                }
                else
                {
                    // Gérer l'erreur si le fichier n'est pas trouvé
                    System.Diagnostics.Debug.WriteLine($"Audio resource not found: {fileName}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
    }
}
