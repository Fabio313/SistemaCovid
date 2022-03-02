using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class FilaPacientes
    {
        public Pessoa Head { get; set; }
        public Pessoa Tail { get; set; }


        public FilaPacientes()
        {
            Head = Tail = null;
        }
        public void Push(Pessoa paciente)
        {
            if (Vazio())
            {
                Head = Tail = paciente;
            }
            else
            {
                paciente.Before = Tail;
                Tail.Next = paciente;
                Tail = paciente;
            }
        }
        public bool Vazio()
        {
            if (Head == null && Tail == null)
                return true;
            return false;
        }
    }
}
