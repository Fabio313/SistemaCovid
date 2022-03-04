using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public int Status { get; set; }//0-Leito,1-Quarentena e 2-Alta
        public int Ticket { get; set; }
        public Triage Triage { get; set; }
        public People Next { get; set; }

        public People(int ticket)
        {
            Ticket = ticket;
        }
        public People(string name, DateTime birth, string cpf)
        {
            Name = name;
            BirthDate = birth;
            Age = AgeCalculate();
            CPF = cpf;
            Next = null;
        }
        private int AgeCalculate()
        {
            return (DateTime.Now - BirthDate).Days/365;
        }
        public override string ToString()
        {
            return "Name: " + Name + "\nAge: " + Age + "\nCPF: " + CPF + "\nTriage: \n" + Triage.ToString();
        }
        public string PreTriage()
        {
            return "Name: " + Name + "\nBirth Date: " + BirthDate.ToString("dd/MM/yyyy") + "\nCPF: " + CPF;
        }
        public string EnterQueue()
        {
            return "Ticket: " + Ticket;
        }
    }
}
