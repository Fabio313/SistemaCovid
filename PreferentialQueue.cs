using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class PreferentialQueue
    {
        public People Head { get; set; }
        public People Tail { get; set; }

        public PreferentialQueue()
        {
            Head = Tail = null;
        }

        public void Push(People register)
        {
            if (Empty())
                Head = Tail = register;
            else
            {
                Tail.Next = register;
                Tail = register;
            }
        }

        public void Print()
        {
            People print = Head;
            do
            {
                Console.WriteLine(print.PreTriage());
                print = print.Next;
            } while (print != null);
        }
        public bool Empty()
        {
            if ((Head == null) && (Tail == null))
                return true;
            return false;
        }
    }
}
