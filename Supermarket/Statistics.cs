using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Statistics
    {
        public List<Registr> statisticList = new List<Registr>();

        public void PrintAllStatistic()
        {
            int pieses = 0;
            int money = 0;
            Console.WriteLine("-----------------------");
            Console.WriteLine("\tStatistic for all days \n");
            if (statisticList.Count != 0)
            {
                for (int i = 0; i < statisticList.Count; i++)
                {

                    Console.WriteLine($"{i + 1}.\t {statisticList[i].day.ToShortDateString()} \t {statisticList[i].name} \t ");
                    pieses++;
                    money += statisticList[i].price;
                }
            }

            Console.WriteLine($"\nTOTAL:");
            Console.WriteLine($"Pieses- {pieses} pcs");
            Console.WriteLine($"Summa- {money} grn");
            Console.WriteLine("-----------------------");
        }
        public void AskStatisticDay()
        {
            DateTime dtOut;
            DateTime time = DateTime.Now;
            Console.WriteLine("-----------------------");

            Console.WriteLine("Input date in format like: {0:d}", new DateTime(2008, 1, 7));
            while (!DateTime.TryParse(Console.ReadLine(), out dtOut))
            {
                Console.WriteLine("Input date in format like: {0:d}", new DateTime(2008, 1, 7));
            }
            PrintStatisticDay(dtOut);
        }

        public void PrintStatisticDay(DateTime dtOut)
        {
            int pieses = 0;
            int money = 0;
            bool existDate = false;

            Console.WriteLine("-----------------------");

            for (int i = 0; i < statisticList.Count; i++)
            {
                if (statisticList[i].day.ToShortDateString() == dtOut.ToShortDateString())
                {
                    existDate = true;
                }
            }


            if (existDate==true)
            {
                Console.WriteLine($"\tStatistic for {dtOut.ToShortDateString()}");
                for (int i = 0; i < statisticList.Count; i++)
                {
                    if (statisticList[i].day.ToShortDateString() == dtOut.ToShortDateString())
                    {
                        Console.WriteLine($"{statisticList[i].day.ToShortDateString()} \t {statisticList[i].name} \t ");
                        pieses++;
                        money += statisticList[i].price;
                    }
                }

                Console.WriteLine($"\nTOTAL:");
                Console.WriteLine($"Pieses- {pieses} pcs");
                Console.WriteLine($"Summa- {money} grn");
            }
            else
            {
                Console.WriteLine($"Not information for {dtOut.ToShortDateString()}");
            }
           
            Console.WriteLine("-----------------------");
        }
    }
}

