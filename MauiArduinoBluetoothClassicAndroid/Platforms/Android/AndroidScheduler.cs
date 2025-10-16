using Android.App;
using Android.Content;
using AndroidX.Work;
using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid.Platforms.Android
{
    class AndroidScheduler : IBackgroundScheduler, IWorkerMessageSender
    {
        public void ScheduleOneTimeTask(Planification planification)
        {
            // 1. Calcul du délai (en millisecondes)
            TimeSpan delay = planification.GetDelai();

            if (delay.TotalMilliseconds < 0)
            {
                // Si l'heure est passée, on recrée le jour après
                planification.ProchaineExecution = planification.ProchaineExecution.AddDays(1);
                delay = (planification.ProchaineExecution - DateTime.Now);
            }

            var inputData = new Data.Builder()
                .PutString("ID_PLANIFICATION", planification.IdPlanification)
                .PutLong("DATETIME", planification.ProchaineExecution.ToBinary())
                .PutInt("MINUTES", planification.MinutesEntreOuvertureEtFermeture)
                .PutBoolean("OUVERT", planification.Ouvert)
                .Build();

            // 2. Création de la requête de travail
            WorkRequest workRequest = new OneTimeWorkRequest.Builder(typeof(BluetoothConnectWorker))
                .SetInitialDelay((long)delay.TotalMilliseconds, TimeUnit.Milliseconds)
                .SetInputData(inputData)
                .AddTag(planification.IdPlanification)
                .Build();

            // 3. Soumission au WorkManager
            WorkManager.GetInstance(global::Android.App.Application.Context)
                 .Enqueue(workRequest);
            //System.Diagnostics.Debug.WriteLine($"WorkManager: Tâche '{uniqueTag}' planifiée dans {delay.TotalMinutes:F0} minutes.");

            TaskTriggerMessage t = new TaskTriggerMessage();
            t.StatusMessage = "Tâche " + planification.IdPlanification + " créée pour exécution à " + planification.ProchaineExecution;
            t.Operation = STATUS.NOTIFICATION;
            MessagingCenter.Send<IWorkerMessageSender, TaskTriggerMessage>((IWorkerMessageSender)this, "TaskCompleted", t);
        }

        public void CancelTask(string idPlanification)
        {
            // Annuler la tâche par son tag
            WorkManager.GetInstance(global::Android.App.Application.Context)
                .CancelAllWorkByTag(idPlanification);
        }
    }
}
