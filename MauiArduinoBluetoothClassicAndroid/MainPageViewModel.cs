using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        // --- 1. Implémentation INotifyPropertyChanged pour les mises à jour UI ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // --- 2. Variables des Textes des Boutons ---

        private string _time1ButtonText = "Actif";
        public string Time1ButtonText
        {
            get => _time1ButtonText;
            set
            {
                if (_time1ButtonText != value)
                {
                    _time1ButtonText = value;
                    OnPropertyChanged(); // Informe l'UI du changement
                }
            }
        }
        private string _time1Color = "#F87B0A";
        public string Time1Color
        { 
            get => _time1Color;
            set
            {
                if ( _time1Color != value)
                {
                    _time1Color = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _time2ButtonText = "Actif";
        public string Time2ButtonText
        {
            get => _time2ButtonText;
            set
            {
                if (_time2ButtonText != value)
                {
                    _time2ButtonText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _time2Color = "#F87B0A";
        public string Time2Color
        {
            get => _time2Color;
            set
            {
                if (_time2Color != value)
                {
                    _time2Color = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _time3ButtonText = "Actif";
        public string Time3ButtonText
        {
            get => _time3ButtonText;
            set
            {
                if (_time3ButtonText != value)
                {
                    _time3ButtonText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _time3Color = "#F87B0A";
        public string Time3Color
        {
            get => _time3Color;
            set
            {
                if (_time3Color != value)
                {
                    _time3Color = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _time4ButtonText = "Actif";
        public string Time4ButtonText
        {
            get => _time4ButtonText;
            set
            {
                if (_time4ButtonText != value)
                {
                    _time4ButtonText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _time4Color = "#F87B0A";
        public string Time4Color
        {
            get => _time4Color;
            set
            {
                if (_time4Color != value)
                {
                    _time4Color = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _debugModeText = "Debug: OFF";
        public string DebugModeText
        {
            get => _debugModeText;
            set
            {
                if (_debugModeText != value)
                { 
                    _debugModeText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _debugModeColor = "#000000";
        public string DebugModeColor
        { 
            get => _debugModeColor;
            set
            {
                if (_debugModeColor != value)
                { 
                    _debugModeColor = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
