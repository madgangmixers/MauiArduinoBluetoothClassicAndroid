using MauiArduinoBluetoothClassicAndroid.Bluetooth;

namespace MauiArduinoBluetoothClassicAndroid;

public partial class MainPage : ContentPage
{
    // Rend le ViewModel accessible si vous en avez besoin, mais le XAML peut y accéder directement.
    private readonly MainPageViewModel _viewModel;

    // Injections des dépendances projet Android
    private readonly IBluetoothConnector _bluetooth;
    private readonly IBackgroundScheduler _scheduler;
    private readonly IAudioPlayerService _audioPlayer;

    // Nom du device bluetooth à rechercher
    const string ArduinoBluetoothTransceiverName = "HC-05";

    // ID uniques pour pouvoir créer/annuler des tâches planifiées
    private const string ID_TASK_1130 = "id_p_1130";
    private const string ID_TASK_1600 = "id_p_1600";
    private const string ID_TASK_2000 = "id_p_2000";
    private const string ID_TASK_2200 = "id_p_2200";

    // Définitions des tâches planifiées seront horaires prédéfinis
    /*Planification P_TASK_1 = new Planification(13, 25, 1, ID_TASK_1130, false);
    Planification P_TASK_2 = new Planification(13, 27, 1, ID_TASK_1600, false);*/
    Planification P_TASK_1 = new Planification(11, 30, 15, ID_TASK_1130, false);
    Planification P_TASK_2 = new Planification(16, 0, 15, ID_TASK_1600, false);
    Planification P_TASK_3 = new Planification(20, 0, 15, ID_TASK_2000, false);
    Planification P_TASK_4 = new Planification(22, 0, 570, ID_TASK_2200, false);

    // Activation/désactivation des tâches planifiées
    private bool alarme11h30 = true;
    private bool alarme16h = true;
    private bool alarme20h = true;
    private bool alarmeNuit = true;

    // Mode debug : Son à la place de commande bluetooth, affichage de dialog lors d'une planification
    private bool debug = false;

    public MainPage(IBluetoothConnector bluetooth, IBackgroundScheduler scheduler, IAudioPlayerService audioPlayer)
    {
        InitializeComponent();

        MessagingCenter.Subscribe<IWorkerMessageSender, TaskTriggerMessage>(
                this,
                "TaskCompleted",
                OnWorkerStatusReceived,
                null // Le thread sur lequel recevoir (null signifie sur le thread UI)
            );

        _viewModel = new MainPageViewModel();
        BindingContext = _viewModel;
        _bluetooth = bluetooth;
        _scheduler = scheduler;
        _audioPlayer = audioPlayer;
    }

    // Méthode appelée lorsque le message est reçu
    private void OnWorkerStatusReceived(IWorkerMessageSender sender, TaskTriggerMessage message)
    {
        if (message.Operation == STATUS.NOTIFICATION)
        {
            // IMPORTANT : Utiliser le Dispatcher pour garantir l'exécution sur le thread UI
            // (Même si MessagingCenter essaie de le faire, c'est la meilleure pratique)
            if (debug)
            {
                Dispatcher.Dispatch(async () =>
                {
                    // Affiche la boîte de dialogue MAUI (qui utilise l'alerte native Android)
                    await DisplayAlert("", message.StatusMessage, "OK");
                });
            }
        }
        else if (message.Operation == STATUS.PERFORM_OPERATION)
        {
            // Le Worker à déclenché, réalisation de la tâche
            if (message.Planif.Ouvert)
            {
                if (debug)
                {
                    PlaySound("close");
                }
                SendData("1");
            }
            else
            {
                if (debug)
                {
                    PlaySound("open");
                }
                SendData("0");
            }

            // Tâche complétée, replanification d'une tâche suivant l'étape suivante et son horaire (ouvrir -> fermer -> ouvrir -> ...)
            message.Planif.ScheduleProchaineExecution();
            _scheduler.ScheduleOneTimeTask(message.Planif);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Demandez les permissions BLUETOOTH à l'exécution si pas encore octroyées.
        PermissionStatus status = await CheckAndRequestBluetoothPermission();

        // Annulation des planifications existantes en cas de redémarrage/réaffichage de l'appli
        _scheduler.CancelTask(ID_TASK_1130);
        _scheduler.CancelTask(ID_TASK_1600);
        _scheduler.CancelTask(ID_TASK_2000);
        _scheduler.CancelTask(ID_TASK_2200);

        // Création/recréations des planifications pour les tâches automatiques SI ACTIVES
        if (alarme11h30)
        {
            _scheduler.ScheduleOneTimeTask(P_TASK_1);
        }

        if (alarme16h)
        {
            _scheduler.ScheduleOneTimeTask(P_TASK_2);
        }

        if (alarme20h)
        {
            _scheduler.ScheduleOneTimeTask(P_TASK_3);
        }

        if (alarmeNuit)
        {
            _scheduler.ScheduleOneTimeTask(P_TASK_4);
        }
    }

    public void SendData(string data) 
    {
        if (debug)
        {
            SendDummyData(data);
        }
        else
        {
            SendRealData(data);
        }
    }

    public async void SendDummyData(string data) 
    {
        Dispatcher.Dispatch(async () =>
        {
            await DisplayAlert("Envoi de données(dummy)", $"Envoi des données: {data}", "OK");
        });
    }

    public async void SendRealData(string data)
    {
        PermissionStatus status = await CheckAndRequestBluetoothPermission();
        if (status == PermissionStatus.Granted)
        {
            try
            {
                // Pour être sûr que l'Activité est bien prête pour les API de bas niveau:
#if ANDROID
                    await Microsoft.Maui.ApplicationModel.Platform.WaitForActivityAsync();
#endif

                // 2. Utilisez Await pour appeler la méthode de manière asynchrone.
                // Cela garantit que le thread ne se bloque pas.
                var connectedDevices = await _bluetooth.GetConnectedDevices();

                var arduino = connectedDevices.FirstOrDefault(d => d == ArduinoBluetoothTransceiverName);

                if (arduino != null)
                {
                    // 3. Connexion à l'appareil
                    _bluetooth.Connect(arduino, data);
                    // Mettez à jour l'interface utilisateur ou les journaux ici...
                }
                else
                {
                    // Gérez le cas où l'appareil n'est pas trouvé
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                await DisplayAlert("Erreur Bluetooth", $"Échec de la connexion Bluetooth: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Permission Requis", "L'accès Bluetooth est nécessaire pour se connecter à l'Arduino.", "OK");
        }
    }

    // Fonction d'assistance pour les permissions
    private async Task<PermissionStatus> CheckAndRequestBluetoothPermission()
    {
        // Utilisez la classe de permission native si elle gère BLUETOOTH_CONNECT (MAUI 8.0+)
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();

        if (status != PermissionStatus.Granted)
        {
            // Tente de demander la permission.
            status = await Permissions.RequestAsync<Permissions.Bluetooth>();
        }
        return status;
    }

    // Événement lancé lorsqu'on clique sur un bouton
    private void TimeButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            string time = button.CommandParameter as string;

            switch (time)
            {
                case "11h30": 
                    alarme11h30 = !alarme11h30;
                    _viewModel.Time1ButtonText = alarme11h30 ? "Actif" : "Inactif";
                    _viewModel.Time1Color = alarme11h30 ? "#F87B0A" : "#333333";
                    if (!alarme11h30)
                    {
                        _scheduler.CancelTask(ID_TASK_1130);
                    }
                    else
                    {
                        P_TASK_1 = new Planification(11, 30, 15, ID_TASK_1130, false);
                        _scheduler.ScheduleOneTimeTask(P_TASK_1);
                    }
                    break;
                case "16h00": 
                    alarme16h = !alarme16h;
                    _viewModel.Time2ButtonText = alarme16h ? "Actif" : "Inactif";
                    _viewModel.Time2Color = alarme16h ? "#F87B0A" : "#333333";
                    if (!alarme16h)
                    {
                        _scheduler.CancelTask(ID_TASK_1600);
                    }
                    else
                    {
                        P_TASK_2 = new Planification(16, 00, 15, ID_TASK_1600, false);
                        _scheduler.ScheduleOneTimeTask(P_TASK_2);
                    }
                    break;
                case "20h00": 
                    alarme20h = !alarme20h;
                    _viewModel.Time3ButtonText = alarme20h ? "Actif" : "Inactif";
                    _viewModel.Time3Color = alarme20h ? "#F87B0A" : "#333333";
                    if (!alarme20h)
                    {
                        _scheduler.CancelTask(ID_TASK_2000);
                    }
                    else
                    {
                        P_TASK_3 = new Planification(20, 0, 15, ID_TASK_2000, false);
                        _scheduler.ScheduleOneTimeTask(P_TASK_3);
                    }
                    break;
                case "22h-7h30": 
                    alarmeNuit = !alarmeNuit;
                    _viewModel.Time4ButtonText = alarmeNuit ? "Actif" : "Inactif";
                    _viewModel.Time4Color = alarmeNuit ? "#F87B0A" : "#333333";
                    if (!alarmeNuit)
                    {
                        _scheduler.CancelTask(ID_TASK_2200);
                    }
                    else
                    {
                        P_TASK_4 = new Planification(22, 0, 570, ID_TASK_2200, false);
                        _scheduler.ScheduleOneTimeTask(P_TASK_4);
                    }
                    break;
            }
        }
    }

    private void OpenButton_Clicked(object sender, EventArgs e)
    {
        PlaySound("open.wav");
        SendData("0");
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        PlaySound("close.wav");
        SendData("1");
    }

    private void DebugMode_Clicked(object sender, EventArgs e)
    {
        this.debug = !this.debug;
        _viewModel.DebugModeText = this.debug ? "Debug: ON" : "Debug: OFF";
        _viewModel.DebugModeColor = this.debug ? "#F87B0A" : "#000000";
    }

    private void PlaySound(string file)
    {
        _audioPlayer?.PlayAudio(file);
    }
}