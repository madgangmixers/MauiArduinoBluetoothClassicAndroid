using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    public class QuestionReponse
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Reponse { get; set; }

        public QuestionReponse(int id, string question, string reponse)
        {
            Id = id;
            Question = question;
            Reponse = reponse;
        }
    }
}
