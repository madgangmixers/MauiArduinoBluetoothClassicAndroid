using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiArduinoBluetoothClassicAndroid
{
    public class Planification
    {
        private const int MINUTES_DANS_JOURNEE = 1440;
        public string IdPlanification { get; set; }
        public int MinutesEntreOuvertureEtFermeture { get; set; }
        public DateTime ProchaineExecution { get; set; }
        public bool Ouvert { get; set; }

        public Planification(int heures, int minutes, int duree, string idPlanification, bool ouvert)
        {
            DateTime now = DateTime.Now;
            ProchaineExecution = new DateTime(now.Year, now.Month, now.Day, heures, minutes, 0);
            MinutesEntreOuvertureEtFermeture = duree;
            IdPlanification = idPlanification;
            Ouvert = ouvert;   
        }

        public DateTime ScheduleProchaineExecution()
        {
            Ouvert = !Ouvert;
            if (Ouvert)
            {
                ProchaineExecution = ProchaineExecution.AddMinutes(MinutesEntreOuvertureEtFermeture);
            }
            else
            {
                ProchaineExecution = ProchaineExecution.AddMinutes(MINUTES_DANS_JOURNEE - MinutesEntreOuvertureEtFermeture);
            }
            return ProchaineExecution;
        }

        public TimeSpan GetDelai()
        {
            return ProchaineExecution - DateTime.Now;
        }
    }
}
