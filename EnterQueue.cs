using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class EnterQueue
    {
        public People Head { get; set; }
        public People Tail { get; set; }
        public int cont { get; set; }

        public EnterQueue()
        {
            cont = 0;
            Head = Tail = null;
        }
        public void Push(People ticket)
        {
            if(Empty())
                Tail = Head = ticket;
            else
            {
                Tail.Next = ticket;
                Tail = ticket;
            }
            cont++;
            ticket.Ticket = cont;
        }
        public void Print()
        {
            People print = Head;
            do
            {
                Console.WriteLine(print.EnterQueue());
                print = print.Next;
            } while (print != null);
        }
        public bool Empty()
        {
            if((Head == null)&&(Tail == null))
                return true;
            return false;
        }
        public void QueueNext()
        {
            if (Empty())
                Console.WriteLine("No one in the queue");
            else
            {
                Head = Head.Next;
            }
        }
    }
}
