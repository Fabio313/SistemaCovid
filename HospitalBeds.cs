using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class HospitalBeds
    {
        public People Head { get; set; }
        public People Tail { get; set; }
        public int Limiter { get; set; }

        public HospitalBeds()
        {
            Head = Tail = null;
        }
        public void Push(People emergency)
        {
            
            if (Empty())
                Head = Tail = emergency;
            else
            {
                Tail.Next = emergency;
                Tail = emergency;
            }
            emergency.Status = 0;
        }
        public void Print()
        {
            People print = Head;
            do
            {
                Console.WriteLine(print.ToString());
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
