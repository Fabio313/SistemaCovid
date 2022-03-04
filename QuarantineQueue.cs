using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospital
{
    internal class QuarantineQueue
    {
        public People Head { get; set; }
        public People Tail { get; set; }

        public QuarantineQueue()
        {
            Head = Tail = null;
        }

        public void Push(People register)
        {
            if (Empty())
            {
                Head = Tail = register;
            }
            else
            {
                if (register.Triage.Comorbidade <= Tail.Triage.Comorbidade)
                {
                    Tail.Next = register;
                    Tail = register;
                }
                else if (register.Triage.Comorbidade > Head.Triage.Comorbidade)
                {
                    register.Next = Head;
                    Head = register;
                }
                else
                {
                    People aux1 = Head;
                    People aux2 = Head;
                    do
                    {
                        if (register.Triage.Comorbidade < aux1.Triage.Comorbidade)
                        {
                            aux2 = aux1;
                            aux1 = aux1.Next;
                        }
                        else
                        {
                            aux2.Next = register;
                            register.Next = aux1;
                        }
                    } while (register.Next == null);
                }
            }
            register.Status = 1;
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
