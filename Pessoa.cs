using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class Pessoa
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public Pessoa Next { get; set; }
        public Pessoa Before { get; set; }
        public Triagem  { get; set; }

        public Pessoa(string name, DateTime birthdate, string cpf, string gender, string address)
        {
            Name = name;
            BirthDate = birthdate;
            CPF = cpf;
            Gender = gender;
            Address = address;
        }

        public override string ToString()
        {
            return "------------------\nName: " + Name + "\nBirth Date: " + BirthDate + "\nCPF: " + CPF + "\nGender: " + "\nAddress: " + Address;
        }
    }
}
