using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    public interface IBackgroundScheduler
    {
        // Méthode pour planifier une tâche unique à une heure cible.
        // Le "tag" permet d'annuler ou de vérifier l'état de la tâche.
        void ScheduleOneTimeTask(Planification planification);

        // Pour annuler le WorkManager (très important)
        void CancelTask(string planification);
    }
}
