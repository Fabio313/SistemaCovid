using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class Triage
    {
        public string Sintomas { get; set; }
        public bool CovidChance { get; set; }
        public int TempoSintomatico { get; set; }
        public int Comorbidade { get; set; }
        public bool Emergencia { get; set; }

        public Triage(string symptoms, bool covidChance, int SymptomsTime, int comorbidity, bool emergency)
        {
            Sintomas = symptoms;
            CovidChance = covidChance;
            TempoSintomatico = SymptomsTime;
            Comorbidade = comorbidity;
            Emergencia = emergency;
        }
        public override string ToString()
        {
            return "Symptoms: " + Sintomas + "\nDays since symptoms start: " + TempoSintomatico + "\nComorbidity: " + Comorbidade;
        }
    }
}
