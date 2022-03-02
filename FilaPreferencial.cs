using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class FilaPreferencial
    {
        public Pessoa Head { get; set; }
        public Pessoa Tail { get; set; }


        public FilaPreferencial()
        {
            Head = Tail = null;
        }
        public void Push(Pessoa preferencial)
        {
            if(Vazio())
            {
                Head = Tail = preferencial;
            }
            else
            {
                preferencial.Before = Tail;
                Tail.Next = preferencial;
                Tail = preferencial;
            }
        }
        public bool Vazio()
        {
            if(Head==null && Tail==null)
                return true;
            return false;
        }
    }
}
