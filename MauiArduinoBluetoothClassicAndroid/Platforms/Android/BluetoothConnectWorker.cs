using Android.Content;
using Android.Runtime;
using AndroidX.Work;
using MauiArduinoBluetoothClassicAndroid.Bluetooth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid.Platforms.Android
{
    // Ciblez votre contexte Android
    //[Worker(Name = "BluetoothConnectWorker")]
    [Register("mauiarduinobluetoothclassicandroid.BluetoothConnectWorker")]
    public class BluetoothConnectWorker : Worker, IWorkerMessageSender
    {
        Data inputData;

        public BluetoothConnectWorker(Context context, WorkerParameters workerParams)
            : base(context, workerParams)
        {
            inputData = workerParams.InputData;
        }

        public override Result DoWork()
        {
            try
            {
                DateTime date = DateTime.FromBinary(inputData.GetLong("DATETIME", -1));
                Planification p = new Planification(date.Hour, date.Minute,
                    inputData.GetInt("MINUTES", -1),
                    inputData.GetString("ID_PLANIFICATION"),
                    inputData.GetBoolean("OUVERT", false));

                TaskTriggerMessage t = new TaskTriggerMessage();
                t.Planif = p;
                t.Operation = STATUS.PERFORM_OPERATION;
                MessagingCenter.Send<IWorkerMessageSender, TaskTriggerMessage>((IWorkerMessageSender)this, "TaskCompleted", t);

                // 2. Succès
                return Result.InvokeSuccess();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Si la tâche échoue, vous pouvez demander à la réessayer plus tard.
                return Result.InvokeRetry();
            }
        }
    }
}
