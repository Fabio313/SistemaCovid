using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class NoCovidQueue
    {
        public People Head { get; set; }
        public People Tail { get; set; }

        public NoCovidQueue()
        {
            Head = Tail = null;
        }
        public void Push(People nocovid)
        {
            if (Empty())
                Head = Tail = nocovid;
            else
            {
                Tail.Next = nocovid;
                Tail = nocovid;
            }
            nocovid.Status = 2;
        }
        public void Print()
        {
            People print = Head;
            do
            {
                Console.WriteLine(print.ToString());
                print = print.Next;
            }while(print != null);
        }

        public bool Empty()
        {
            if ((Head == null) && (Tail == null))
                return true;
            return false;
        }
    }
}
