using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    public class TaskTriggerMessage
    {
        public string StatusMessage { get; set; }
        public STATUS Operation { get; set; }
        public Planification Planif { get; set; }
    }

    public enum STATUS
    {
        NOTIFICATION,
        PERFORM_OPERATION
    }
}
