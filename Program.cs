using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace SistemaHospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int ticket = 1;
            int opc, contPreferencial = 0;
            BuildArchives();

            EnterQueue enter = new EnterQueue();
            PreferentialQueue preferential = new PreferentialQueue();
            NoPreferentialQueue nopreferential = new NoPreferentialQueue();
            NoCovidQueue nocovid = new NoCovidQueue();
            QuarantineQueue quarantine = new QuarantineQueue();
            HospitalBeds hospitalbeds = new HospitalBeds();
            HospitalBedsQueue hospitalBedsQueue = new HospitalBedsQueue();
            Console.WriteLine("Digite quantos leitos o hospital tem disponivel: ");
            hospitalbeds.Limiter = int.Parse(Console.ReadLine());
            Console.WriteLine("Is a new day(1-yes and 2-no): ");
            int newday = int.Parse(Console.ReadLine());
            if (newday == 1)
                NewDayArchives(hospitalbeds,hospitalBedsQueue,nocovid);
            else
                SameDayArchives(preferential,nopreferential,nocovid,quarantine,hospitalbeds,hospitalBedsQueue);
            do
            {
                opc = Menu();
                switch (opc)
                {
                    case 1:
                        People aux = Register(enter.Head.Ticket);
                        if (enter.Empty())
                        {
                            Console.WriteLine("No one is on the queue");
                        }
                        else
                        {
                            enter.QueueNext();
                            if (aux.Age >= 60)
                            {
                                preferential.Push(aux);
                                try
                                {
                                    StreamWriter sw = new StreamWriter("Preferential.txt", append: true);
                                    sw.WriteLine(aux.Name + ";" + aux.BirthDate.ToString("dd/MM/yyyy") + ";" + aux.CPF);
                                    sw.Close();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: " + e.Message);
                                }
                            }
                            else
                            {
                                nopreferential.Push(aux);
                                try
                                {
                                    StreamWriter sw = new StreamWriter("NoPreferential.txt", append: true);
                                    sw.WriteLine(aux.Name + ";" + aux.BirthDate.ToString("dd/MM/yyyy") + ";" + aux.CPF);
                                    sw.Close();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Exception: " + e.Message);
                                }
                            }
                        }
                        break;

                    case 2:
                        contPreferencial = MakeTriage(contPreferencial, hospitalBedsQueue, preferential, nopreferential, nocovid, quarantine, hospitalbeds);
                        break;
                    case 3:
                        if (!hospitalbeds.Empty())
                            hospitalbeds.Print();
                        else
                            Console.WriteLine("No peoples in the hospital beds list");
                        break;
                    case 4://se tiver apenas um tipo de preferencia aparece a msg de nao existir junto com a fila que existe
                        if (!preferential.Empty())
                            preferential.Print();
                        if (!nopreferential.Empty())
                            nopreferential.Print();
                        if (preferential.Empty()&& preferential.Empty())
                            Console.WriteLine("No peoples in the triage list");
                        break;
                    case 5:
                        if(!enter.Empty())
                            enter.Print();
                        else
                            Console.WriteLine("No peoples in the enter list");
                        break;
                    case 6:
                        if (!quarantine.Empty())
                            quarantine.Print();
                        else
                            Console.WriteLine("No peoples in the quarantine list");
                        break;
                    case 7:
                        break;
                    case 8:
                        if (!nocovid.Empty())
                            nocovid.Print();
                        else
                            Console.WriteLine("No peoples in the no covid historic");
                        break;
                    case 9:
                        enter.Push(new People(ticket));
                        Console.WriteLine("New Person Arrives and get 1 ticket");
                        break;
                    case 0:

                        break;
                    default:

                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (opc != 0);

        }

        private static void BuildArchives()
        {
            if (!File.Exists("Registers.txt"))
            {
                StreamWriter sw = new StreamWriter("Registers.txt");
                sw.Close();
            }
            if (!File.Exists("Triage.txt"))
            {
                StreamWriter sw = new StreamWriter("Triage.txt");
                sw.Close();
            }

            if (!File.Exists("Preferential.txt"))
            {
                StreamWriter sw = new StreamWriter("Preferential.txt");
                sw.Close();
            }
            if (!File.Exists("NoPreferential.txt"))
            {
                StreamWriter sw = new StreamWriter("NoPreferential.txt");
                sw.Close();
            }
            if (!File.Exists("HospitalBeds.txt"))
            {
                StreamWriter sw = new StreamWriter("HospitalBeds.txt");
                sw.Close();
            }
            if (!File.Exists("HospitalBedsQueue.txt"))
            {
                StreamWriter sw = new StreamWriter("HospitalBedsQueue.txt");
                sw.Close();
            }
            if (!File.Exists("Quarentine.txt"))
            {
                StreamWriter sw = new StreamWriter("Quarentine.txt");
                sw.Close();
            }
            if (!File.Exists("NoCovidHistoric.txt"))
            {
                StreamWriter sw = new StreamWriter("NoCovidHistoric.txt");
                sw.Close();
            }
        }
        private static void NewDayArchives(HospitalBeds hospitalBeds, HospitalBedsQueue hospitalBedsQueue, NoCovidQueue nocovid)
        {
            StreamReader sr = new StreamReader("HospitalBeds.txt");//Preferencial
            string line = sr.ReadLine();
            while (line != null)
            {
                StreamReader sr2 = new StreamReader("Triage.txt");
                string line2 = sr2.ReadLine();
                string[] peoples = line.Split(';');
                string[] triage = new string[5]; ;
                do
                {
                    if (line2.Contains(peoples[2]))
                    {
                        triage = line2.Split(';');
                        break;
                    }
                    line2 = sr2.ReadLine();
                } while (line2 != null);
                sr2.Close();
                hospitalBeds.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                hospitalBeds.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                line = sr.ReadLine();

            }
            sr.Close();

            sr = new StreamReader("HospitalBedsQueue.txt");//Preferencial
            line = sr.ReadLine();
            while (line != null)
            {
                StreamReader sr2 = new StreamReader("Triage.txt");
                string line2 = sr2.ReadLine();
                string[] peoples = line.Split(';');
                string[] triage = new string[5]; ;
                do
                {
                    if (line2.Contains(peoples[2]))
                    {
                        triage = line2.Split(';');
                        break;
                    }
                    line2 = sr2.ReadLine();
                } while (line2 != null);
                sr2.Close();
                hospitalBedsQueue.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                hospitalBedsQueue.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                line = sr.ReadLine();

            }
            sr.Close();

            sr = new StreamReader("NoCovidHistoric.txt");//Preferencial
            line = sr.ReadLine();
            while (line != null)
            {
                StreamReader sr2 = new StreamReader("Triage.txt");
                string line2 = sr2.ReadLine();
                string[] peoples = line.Split(';');
                string[] triage = new string[5];
                do
                {
                    if (line2.Contains(peoples[2]))
                    {
                        triage = line2.Split(';');
                        break;

                    }
                    line2 = sr2.ReadLine();
                } while (line2 != null);
                sr2.Close();
                nocovid.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                nocovid.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                line = sr.ReadLine();

            }
            sr.Close();
        }
        private static void SameDayArchives(PreferentialQueue preferential, NoPreferentialQueue noPreferential, NoCovidQueue nocovid, QuarantineQueue quarantine, HospitalBeds hospitalBeds,HospitalBedsQueue hospitalBedsQueue)
        {
            try
            {
                StreamReader sr = new StreamReader("Preferential.txt");//Preferencial
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] peoples = line.Split(';');
                    preferential.Push(new People(peoples[0], DateTime.Parse(peoples[1]),peoples[2]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("NoPreferential.txt");//nao preferencial
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] peoples = line.Split(';');
                    preferential.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("NoCovidHistoric.txt");//Preferencial
                line = sr.ReadLine();
                while (line != null)
                {
                    StreamReader sr2 = new StreamReader("Triage.txt");
                    string line2 = sr2.ReadLine();
                    string[] peoples = line.Split(';');
                    string[] triage = new string[5];
                    do
                    {
                        if (line2.Contains(peoples[2]))
                        {
                            triage = line2.Split(';');
                            break;

                        }
                        line2 = sr2.ReadLine();
                    } while (line2 != null);
                    sr2.Close();
                    nocovid.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                    nocovid.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                    
                }
                sr.Close();
                

                sr = new StreamReader("Quarentine.txt");//Preferencial
                line = sr.ReadLine();
                while (line != null)
                {
                    StreamReader sr2 = new StreamReader("Triage.txt");
                    string line2 = sr2.ReadLine();
                    string[] peoples = line.Split(';');
                    string[] triage = new string[5]; ;
                    do
                    {
                        if (line2.Contains(peoples[2]))
                        {
                            triage = line2.Split(';');
                            break;
                        }
                        line2 = sr2.ReadLine();
                    } while (line2 != null);
                    sr2.Close();
                    quarantine.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                    quarantine.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                    
                }
                sr.Close();

                sr = new StreamReader("HospitalBeds.txt");//Preferencial
                line = sr.ReadLine();
                while (line != null)
                {
                    StreamReader sr2 = new StreamReader("Triage.txt");
                    string line2 = sr2.ReadLine();
                    string[] peoples = line.Split(';');
                    string[] triage = new string[5]; ;
                    do
                    {
                        if (line2.Contains(peoples[2]))
                        {
                            triage = line2.Split(';');
                            break;
                        }
                        line2 = sr2.ReadLine();
                    } while (line2 != null);
                    sr2.Close();
                    hospitalBeds.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                    hospitalBeds.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                    
                }
                sr.Close();

                sr = new StreamReader("HospitalBedsQueue.txt");//Preferencial
                line = sr.ReadLine();
                while (line != null)
                {
                    StreamReader sr2 = new StreamReader("Triage.txt");
                    string line2 = sr2.ReadLine();
                    string[] peoples = line.Split(';');
                    string[] triage = new string[5]; ;
                    do
                    {
                        if (line2.Contains(peoples[2]))
                        {
                            triage = line2.Split(';');
                            break;
                        }
                        line2 = sr2.ReadLine();
                    } while (line2 != null);
                    sr2.Close();
                    hospitalBedsQueue.Push(new People(peoples[0], DateTime.Parse(peoples[1]), peoples[2]));
                    hospitalBedsQueue.Tail.Triage = new Triage(triage[1], bool.Parse(triage[2]), int.Parse(triage[3]), int.Parse(triage[4]), bool.Parse(triage[5]));//da um push com as informações nos arquivos
                    line = sr.ReadLine();
                    
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        private static int MakeTriage(int contPreferencial,HospitalBedsQueue hospitalBedsQueue,PreferentialQueue preferential, NoPreferentialQueue nopreferential, NoCovidQueue nocovid, QuarantineQueue quarentine, HospitalBeds hospitalBeds)
        {
            
            int selectsymptoms = 0;
            string symptoms = "";
            int daysymptom;
            bool CovidChanse = false, emergency = false;
            int comorbiditys;
            int cont=0;
            do
            {
                Console.WriteLine("Chose the pacient symptoms: \n1-Fever\n2-Loss of taste\n3-Loss of smell\n4-Headache\n0-Exit");
                selectsymptoms = int.Parse(Console.ReadLine());
                if ((selectsymptoms != 0) && (cont!=0))
                {
                    symptoms += ",";
                }
                switch (selectsymptoms)
                {
                    case 1:
                        symptoms += "Fever";
                        cont++;
                        break;
                    case 2:
                        symptoms += "Loss of taste";
                        cont++;
                        break;
                    case 3:
                        symptoms += "Loss of smell";
                        cont++;
                        break;
                    case 4:
                        symptoms += "Headache";
                        cont++;
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid Option!");
                        break;
                }
            } while (selectsymptoms != 0);
            Console.WriteLine("Write how many days de symptoms has appear: ");
            daysymptom = int.Parse(Console.ReadLine());
            if (daysymptom >= 3)
                CovidChanse = true;
            Console.WriteLine("Write how much comorbiditys the patient has: ");
            comorbiditys = int.Parse(Console.ReadLine());
            Console.WriteLine("Is the paticient in emergency(1-yes e 2-no): ");
            if (int.Parse(Console.ReadLine()) == 1)
                emergency = true;
            //cria triagem com as informaçoes perguntadas
            Triage triage = new Triage(symptoms, CovidChanse, daysymptom, comorbiditys, emergency);
            //verifica se é vez do idoso ou nao
            if (contPreferencial < 2 && preferential.Head != null)
            {
                preferential.Head.Triage = triage;
                try
                {
                    StreamWriter sw = new StreamWriter("Triage.txt", append: true);//salva as informações da triagem junto com o cpf da pessoa
                    sw.WriteLine(preferential.Head.CPF + ";" + preferential.Head.Triage.Sintomas + ";" + preferential.Head.Triage.CovidChance + ";" + preferential.Head.Triage.TempoSintomatico + ";" + preferential.Head.Triage.Comorbidade + ";" + preferential.Head.Triage.Emergencia);
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                contPreferencial++;
                string information = preferential.Head.Name + ";" + preferential.Head.BirthDate.ToString("dd/MM/yyyy") + ";" + preferential.Head.CPF + ";" + preferential.Head.Triage.Sintomas + ";" + preferential.Head.Triage.CovidChance + ";" + preferential.Head.Triage.TempoSintomatico + ";" + preferential.Head.Triage.Comorbidade + ";" + preferential.Head.Triage.Emergencia;
                //se menos de 3 dias de sintomas nao tem covid
                if ((preferential.Head.Triage.TempoSintomatico < 3) || (symptoms == ""))
                {
                    nocovid.Push(preferential.Head);
                    try
                    {
                        StreamWriter sw = new StreamWriter("NoCovidHistoric.txt", append: true);//salva as informações da triagem junto com o cpf da pessoa
                        sw.WriteLine(information);
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
                else
                {
                    //se estiver em emergencia
                    if (preferential.Head.Triage.Emergencia == true)
                    {
                        if (hospitalBeds.Limiter < 1)//se nao tiver leitos disponiveis vai pra fila de leitos
                        {
                            Console.WriteLine("No Hospital Beds available for the emergency!");
                            hospitalBedsQueue.Push(preferential.Head);
                            try
                            {
                                StreamWriter sw = new StreamWriter("HospitalBedsQueue.txt", append: true);//salva as informações da pessoa na fila de espera para leitos
                                sw.WriteLine(information);
                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: " + e.Message);
                            }
                        }
                        else//se tiver leitos vai pro leito
                        {
                            hospitalBeds.Push(preferential.Head);
                            try
                            {
                                StreamWriter sw = new StreamWriter("HospitalBeds.txt", append: true);//salva as informações  pessoa em leitos
                                sw.WriteLine(information);
                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: " + e.Message);
                            }
                        }
                    }
                    else//se nao for emergencia vai pra quarentena
                    {
                        quarentine.Push(preferential.Head);
                        try
                        {
                            StreamWriter sw = new StreamWriter("Quarentine.txt", append: true);//salva as informações  pessoa de quarentena
                            sw.WriteLine(information);
                            sw.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception: " + e.Message);
                        }
                    }
                }
                preferential.Head = preferential.Head.Next;
                if (preferential.Head == null)
                    preferential.Tail = null;
                if (!preferential.Empty())
                {
                    try//quando o primeiro da lista sai preciso reescrever o arquivo mas agr sem o antigo primeiro
                    {
                        People scan = preferential.Head;
                        StreamWriter sw = new StreamWriter("Preferential.txt");
                        while(scan != null)
                        {
                            sw.WriteLine(scan.Name + ";" + scan.BirthDate.ToString("dd/MM/yyyy") + ";" + scan.CPF);
                            scan = scan.Next;
                        }
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
                else
                {
                    try//quando o primeiro da lista sai preciso reescrever o arquivo mas agr sem o antigo primeiro
                    {
                        StreamWriter sw = new StreamWriter("Preferential.txt");
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }

            }

            else if (!nopreferential.Empty()) //tudo igual porem agora para nao idoso
            {
                nopreferential.Head.Triage = triage;
                try
                {
                    StreamWriter sw = new StreamWriter("Triage.txt", append: true);
                    sw.WriteLine(nopreferential.Head.CPF + ";" + nopreferential.Head.Triage.Sintomas + ";" + nopreferential.Head.Triage.CovidChance + ";" + preferential.Head.Triage.TempoSintomatico + ";" + nopreferential.Head.Triage.Comorbidade + ";" + nopreferential.Head.Triage.Emergencia);
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                contPreferencial++;
                string information = nopreferential.Head.Name + "; " + nopreferential.Head.BirthDate.ToString("dd / MM / yyyy") + "; " + nopreferential.Head.CPF + "; " + nopreferential.Head.Triage.Sintomas + "; " + nopreferential.Head.Triage.CovidChance + "; " + nopreferential.Head.Triage.TempoSintomatico + "; " + nopreferential.Head.Triage.Comorbidade + "; " + nopreferential.Head.Triage.Emergencia;

                if ((nopreferential.Head.Triage.TempoSintomatico < 3) || (symptoms == ""))
                {
                    nocovid.Push(nopreferential.Head);
                    try
                    {
                        StreamWriter sw = new StreamWriter("NoCovidHistoric.txt", append: true);
                        sw.WriteLine(information);
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
                else
                {

                    if (nopreferential.Head.Triage.Emergencia == true)
                    {
                        if (hospitalBeds.Limiter < 1)
                        {
                            Console.WriteLine("No Hospital Beds available for the emergency!");
                            hospitalBedsQueue.Push(nopreferential.Head);
                            try
                            {
                                StreamWriter sw = new StreamWriter("HospitalBedsQueue.txt", append: true);
                                sw.WriteLine(information);
                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: " + e.Message);
                            }
                        }
                        else
                        {
                            hospitalBeds.Push(nopreferential.Head);
                            try
                            {
                                StreamWriter sw = new StreamWriter("HospitalBeds.txt", append: true);
                                sw.WriteLine(information);
                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: " + e.Message);
                            }
                        }
                    }
                    else
                    {
                        quarentine.Push(nopreferential.Head);
                        try
                        {
                            StreamWriter sw = new StreamWriter("Quarentine.txt", append: true);
                            sw.WriteLine(information);
                            sw.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception: " + e.Message);
                        }
                    }
                }
                nopreferential.Head = nopreferential.Head.Next;
                if (nopreferential.Head == null)
                    nopreferential.Tail = null;
                if (!nopreferential.Empty())
                {
                    try//quando o primeiro da lista sai preciso reescrever o arquivo mas agr sem o antigo primeiro
                    {
                        People scan = nopreferential.Head;
                        StreamWriter sw = new StreamWriter("NoPreferential.txt");
                        while (scan != null)
                        {
                            sw.WriteLine(scan.Name + ";" + scan.BirthDate.ToString("dd/MM/yyyy") + ";" + scan.CPF);
                            scan = scan.Next;
                        }
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
                else
                {
                    try//quando o primeiro da lista sai preciso reescrever o arquivo mas agr sem o antigo primeiro
                    {
                        StreamWriter sw = new StreamWriter("NoPreferential.txt");
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                    }
                }
            }
            else
                Console.WriteLine("No people in the triage queue");

            if (nopreferential.Empty())
                contPreferencial = 0;
            return contPreferencial;
        }

        static int Menu()
        {
            int opc;
            Console.WriteLine("=========PAPINI COVID CENTER=========");
            Console.WriteLine("1-Next number\n2-Next triage patient\n3-Show Hospital beds pacients\n4-Show triage list\n5-Show Reception list(tickets)\n6-Show quarentine list\n7-Show Hospital beds list\n8-Show no covid historic\n9-New person arrives\n0-Exit");
            Console.Write("Option: ");
            if (int.TryParse(Console.ReadLine(), out int CanParse))//ver se o numero digitado é valido
            {
                opc = CanParse;
            }
            else//se nao for vai retornar -1 que cai no default do switch
            {
                opc = -1;
            }
            return opc;
        }
        static People Register(int ticket)
        {
            string name="", cpf;
            DateTime age = new DateTime();
            Console.WriteLine("Write pacient's cpf: ");
            cpf = Console.ReadLine().Trim();
            string line;
            try
            {
                StreamReader sr = new StreamReader("Registers.txt");
                line = sr.ReadLine();

                while(line!=null)
                {
                    if(line.Contains(cpf))//achar alguem com o cpf ja cadastrado
                    {
                        string[] register = line.Split(';');
                        name = register[0];
                        age = DateTime.Parse(register[1]);
                        sr.Close();
                        return new People(name, age, cpf);//cria o objeto com as informações cadastradas
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            //se nao faz um novo cadastro
            Console.WriteLine("Write pacient's name: ");
            name = Console.ReadLine().Trim();
            Console.WriteLine("Write pacient's birth date(dd/mm/aaaa): ");
            age = DateTime.Parse(Console.ReadLine());
            try
            {
                StreamWriter sw = new StreamWriter("Registers.txt", append: true);
                sw.WriteLine(name + ";" + age.ToString("dd/MM/yyyy") + ";" + cpf);
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return new People(name,age,cpf);
        }
    }
}
