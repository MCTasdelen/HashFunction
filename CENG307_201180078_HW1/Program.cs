using System;
using System.Collections;
using System.Collections.Generic;


class CENG307_201180078_HW1
{
    static int TABLE_SIZE; // Hash tablosu boyutu
    const int MAX_recordS = 100;
    class NodeChain
    {
        public int record;
        public int? nof;


    }
    class NodeBeisch
    {
        public int record;
        public int? link;


    }

    static NodeChain[] hashTableChain;
    static NodeBeisch[] hashTableBeisch;
    static int flagbeisch = 0;
    static void Insertbeisch(int record)
    {
        int index = record % TABLE_SIZE;
        if (hashTableBeisch[index] == null)
        {
            hashTableBeisch[index] = new NodeBeisch();
            hashTableBeisch[index].record = record;
        }
        else
        {
            if (flagbeisch % 2 == 0)
            {
                for (int endIndex = TABLE_SIZE - 1; endIndex >= 0; endIndex--)
                {
                    if (hashTableBeisch[endIndex] == null)
                    {
                        hashTableBeisch[endIndex] = new NodeBeisch();
                        hashTableBeisch[endIndex].record = record;
                        if (hashTableBeisch[index].link == null)
                        {
                            hashTableBeisch[index].link = endIndex;
                        }
                        else
                        {
                            hashTableBeisch[endIndex].link = hashTableBeisch[index].link;
                            hashTableBeisch[index].link = endIndex;
                        }
                        break;
                    }

                }
                flagbeisch++;

            }
            else
            {
                for (int startIndex = 0; startIndex < TABLE_SIZE; startIndex++)
                {
                    if (hashTableBeisch[startIndex] == null)
                    {
                        hashTableBeisch[startIndex] = new NodeBeisch();
                        hashTableBeisch[startIndex].record = record;
                        if (hashTableBeisch[index].link == null)
                        {
                            hashTableBeisch[index].link = startIndex;
                        }
                        else
                        {
                            hashTableBeisch[startIndex].link = hashTableBeisch[index].link;
                            hashTableBeisch[index].link = startIndex;
                        }
                        break;
                    }

                }
                flagbeisch++;
            }

        }





    }

    static void InsertChain(int record)
    {
        int index = record % TABLE_SIZE;
        if (hashTableChain[index] == null)
        {
            hashTableChain[index] = new NodeChain();
            hashTableChain[index].record = record;
        }
        else
        {
            if (hashTableChain[index].record % TABLE_SIZE == index)
            {
                if (hashTableChain[index].nof == null)
                {
                    int quot = hashTableChain[index].record / TABLE_SIZE;
                    for (int i = 0; i < TABLE_SIZE; i++)
                    {
                        int loc = ((i * quot) + index) % TABLE_SIZE;
                        if (hashTableChain[loc] == null || hashTableChain[loc].record == 0)
                        {
                            hashTableChain[loc] = new NodeChain();
                            hashTableChain[loc].record = record;
                            hashTableChain[index].nof = i;
                            break;
                        }
                    }
                }
                else
                {
                    int tempIndex = index;
                    for (int p = 0; p < TABLE_SIZE; p++)
                    {
                        int quot = hashTableChain[tempIndex].record / TABLE_SIZE;
                        tempIndex = ((hashTableChain[tempIndex].nof.Value * quot) + tempIndex) % TABLE_SIZE;
                        if (hashTableChain[tempIndex].nof == null)
                        {
                            int quot2 = hashTableChain[tempIndex].record / TABLE_SIZE;
                            for (int i = 0; i < TABLE_SIZE; i++)
                            {
                                int loc = ((i * quot2) + tempIndex) % TABLE_SIZE;
                                if (hashTableChain[loc] == null || hashTableChain[loc].record == 0)
                                {
                                    hashTableChain[loc] = new NodeChain();
                                    hashTableChain[loc].record = record;
                                    hashTableChain[tempIndex].nof = i;
                                    break;
                                }
                            }
                            break;
                        }



                    }



                }
            }
            else
            {

                int[] temp = new int[TABLE_SIZE];
                int index2 = hashTableChain[index].record % TABLE_SIZE;
                int tempIndex = index2;
                int temp3 = hashTableChain[tempIndex].record;
                for (int i = 0; i < TABLE_SIZE; i++)
                {
                    int quot = temp3 / TABLE_SIZE;
                    if (hashTableChain[tempIndex].nof != null)
                    {

                        int temp2 = tempIndex;
                        tempIndex = ((hashTableChain[tempIndex].nof.Value * quot) + tempIndex) % TABLE_SIZE;
                        temp[i] = hashTableChain[tempIndex].record;
                        temp3 = hashTableChain[tempIndex].record;
                        hashTableChain[tempIndex].record = 0;
                        hashTableChain[temp2].nof = null;

                    }
                    else
                    {
                        break;
                    }


                }



                hashTableChain[index].record = record;
                if (hashTableChain[index].nof != null)
                {
                    hashTableChain[index].nof = null;
                }


                for (int i = 0; i < TABLE_SIZE; i++)
                {
                    if (temp[i] != 0)
                    {
                        InsertChain(temp[i]);

                    }
                }




            }

        }
    }
    static void PrintTableChain()
    {
        Console.WriteLine("COMPUTED_CHAIN\nINDEX\tRECORD\tNOF\tProbe");
        int count = 0;
        int totalProbe = 0;
        int flagPF = 0;
        foreach (var hash in hashTableChain)
        {
            if (hash == null)
            {
                Console.WriteLine(count + "\t" + 0);
                flagPF++;
            }
            else
            {

                int probe = 1;
                int index = hash.record % TABLE_SIZE;
                for (int i = 0; i < TABLE_SIZE; i++)
                {
                    if (hashTableChain[index] == null)
                    {
                        break;
                    }
                    else if (hashTableChain[index].record == hash.record)
                    {
                        //Console.WriteLine("Bulundu:\n Index:" + index + "\t" + hashTableChain[index].record + " Probe:" + probe);
                        //flagChain++;
                        break;
                    }


                    int quot = hashTableChain[index].record / TABLE_SIZE;
                    index = ((quot * hashTableChain[index].nof.Value) + index) % TABLE_SIZE;
                    probe++;

                }
                Console.Write(count + " ");
                Console.WriteLine("\t" + hash.record + "\t" + hash.nof + "\t" + probe);
                totalProbe += probe;
            }
            count++;
        }
        Console.WriteLine("TotalProbeChain: " + totalProbe);
        double averagechain = (double)totalProbe / (TABLE_SIZE - flagPF);
        Console.WriteLine("Average of Computed_Chain: " + averagechain);

        double pf = (double)(TABLE_SIZE - flagPF) / TABLE_SIZE * 100;
        string formattedNumber = pf.ToString("F2");
        Console.WriteLine("Packing factor:(" + formattedNumber + ")");
        Console.WriteLine("-----------------------------------");
    }
    static void PrintTableBeisch()
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("BEISCH\nINDEX\tRECORD\tLINK\tPROBE");
        int count = 0;
        int totalProbe = 0;
        int flagPF = 0;
        foreach (var hash in hashTableBeisch)
        {
            if (hash == null)
            {
                Console.WriteLine(count + "\t" + 0);
                flagPF++;
            }
            else
            {
                int index = hash.record % TABLE_SIZE;
                int probe = 1;

                for (int i = 0; i < TABLE_SIZE; i++)
                {
                    if (hashTableBeisch[index] == null)
                    {
                        break;
                    }
                    else if (hashTableBeisch[index].record == hash.record)
                    {
                        //Console.WriteLine("Bulundu:\n Index:" + index + "\t" + hashTableBeisch[index].record + " Probe: " + probe);
                        //flag2beisch++;
                        break;
                    }
                    else if (hashTableBeisch[index].link != null)
                    {
                        index = hashTableBeisch[index].link.Value;
                        probe++;

                    }

                }
                Console.Write(count + " ");
                Console.WriteLine("\t" + hash.record + "\t" + hash.link + "\t" + probe);
                totalProbe += probe;

            }
            count++;
        }
        Console.WriteLine("TotalProbeBeisch: " + totalProbe);
        double averagebeisch = (double)totalProbe / (TABLE_SIZE - flagPF);
        Console.WriteLine("Average of Beisch: " + averagebeisch);

        double pf = (double)(TABLE_SIZE - flagPF) / TABLE_SIZE * 100;
        string formattedNumber = pf.ToString("F2");
        Console.WriteLine("Packing factor:(" + formattedNumber + ")");
    }
    static void PopulateTable(int input)
    {
        Random rand = new Random();
        for (int i = 0; i < input; i++)
        {
            int record = rand.Next(1, MAX_recordS * 10); // Rastgele anahtar oluştur
            InsertChain(record); // Anahtarı tabloya ekle
            Insertbeisch(record);
        }
    }
    static int flagChain = 0;

    static int SearchChain(int record)
    {
        int probe = 1;
        int index = record % TABLE_SIZE;
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            if (hashTableChain[index] == null)
            {
                return 0;
            }
            else if (hashTableChain[index].record == record)
            {
                Console.WriteLine("Computed chain icinde Bulundu:\n Index:" + index + "\t" + hashTableChain[index].record + " Probe:" + probe);
                flagChain++;
                return probe;
            }


            int quot = hashTableChain[index].record / TABLE_SIZE;
            if (hashTableChain[index].nof != null)
            {
                index = ((quot * hashTableChain[index].nof.Value) + index) % TABLE_SIZE;
            }

            probe++;

        }
        return probe;
    }
    static int flag2beisch = 0;
    static int SearchBeisch(int record)
    {
        int index = record % TABLE_SIZE;
        int probe = 1;
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            if (hashTableBeisch[index] == null)
            {
                return 0;
            }
            else if (hashTableBeisch[index].record == record)
            {
                Console.WriteLine("Beisch icinde Bulundu:\n Index:" + index + "\t" + hashTableBeisch[index].record + " Probe: " + probe);
                flag2beisch++;
                return probe;
            }
            else if (hashTableBeisch[index].link != null)
            {
                index = hashTableBeisch[index].link.Value;
                probe++;
            }

        }
        return probe;

    }
    static int FindNextPrime(int number)
    {
        int next = number + 1;
        while (!IsPrime(next))
        {
            next++;
        }
        return next;
    }
    static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Kac eleman olacagini belirtiniz");
        int userInput = Convert.ToInt32(Console.ReadLine());
        int primeCount = 0;
        int currentNumber = userInput;
        while (primeCount < 2)
        {
            currentNumber = FindNextPrime(currentNumber);
            primeCount++;
        }
        TABLE_SIZE = currentNumber;
        hashTableChain = new NodeChain[TABLE_SIZE];
        hashTableBeisch = new NodeBeisch[TABLE_SIZE];

        Console.WriteLine("Table_size= " + TABLE_SIZE);
        PopulateTable(userInput); // Tabloyu doldur

        PrintTableChain(); // Tabloyu ekrana yazdır
        PrintTableBeisch();
        Console.WriteLine("Aramak istediginiz degeri girin");
        int search = Convert.ToInt32(Console.ReadLine());
        int probeChain = SearchChain(search);
        if (flagChain == 0)
        {
            Console.WriteLine("COMPUTED_CHAİN icinde Bulunamadi");
        }
        int probeBeisch = SearchBeisch(search);
        if (flag2beisch == 0)
        {
            Console.WriteLine("BEISCH icinde Bulunamadi");
        }
        if (probeChain == 0 || probeBeisch == 0)
        {
            Console.WriteLine("Karsilastirma yapilamadi");
        }
        else if (probeBeisch < probeChain)
        {
            Console.WriteLine("Beisch algoritmasi daha basarili");
        }
        else if (probeBeisch > probeChain)
        {
            Console.WriteLine("Computed_Chain algoritmasi daha basarili");
        }
        else
        {
            Console.WriteLine("Algoritmlar esit probe'larda veriyi bulmustur");
        }
    }
}