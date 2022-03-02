using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class FilaNaoPreferencial
    {
        public Pessoa Head { get; set; }
        public Pessoa Tail { get; set; }


        public FilaNaoPreferencial()
        {
            Head = Tail = null;
        }
        public void Push(Pessoa naopreferencial)
        {
            if (Vazio())
            {
                Head = Tail = naopreferencial;
            }
            else
            {
                naopreferencial.Before = Tail;
                Tail.Next = naopreferencial;
                Tail = naopreferencial;
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
