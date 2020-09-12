using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Buyer
    {
        public List<Product> buyerProductList;
        // Кошелек
        public int purse; 

        public Buyer()
        {
            buyerProductList = new List<Product>();
            Random random = new Random();
            purse = random.Next(200, 500);
        }

        public void GenerationCheck()
        {
            int summa=0;
            Console.WriteLine("\n Seller: Here is your receipt\n");

            Console.WriteLine("| -----------S H O P--------------");
            Console.WriteLine("| --------------------------------");
            for (int i = 0; i < buyerProductList.Count; i++)
            {
                if (buyerProductList[i].quantity!=0)
                {
                    Console.WriteLine($"| {i+1}.{buyerProductList[i].name}___" +
                   $"{buyerProductList[i].quantity}*{buyerProductList[i].price}" +
                   $"={buyerProductList[i].quantity * buyerProductList[i].price}");
                    summa += buyerProductList[i].quantity * buyerProductList[i].price;
                }
            }
            Console.WriteLine("| --------------------------------");
            Console.WriteLine($"| TOTAL    - {summa}");
            Console.WriteLine($"| You take - {purse}");
            Console.WriteLine($"| Change   - {purse- summa}");
            Console.WriteLine("| ---Thank for shopping here---");
            Console.WriteLine("| --------------------------------");
        }

        public void DisplayBuyer()
        {       
            for (int k = 0; k < buyerProductList.Count; k++)
            {
                Console.WriteLine($"{k + 1} {buyerProductList[k].name}- {buyerProductList[k].quantity}");           
            }
        }

        public bool CheckSumma()
        {
            bool hasMoney = false;
            int summa = 0;
                  Console.WriteLine(" Customer: Can i have some products, please...\n ");
            for (int k = 0; k < buyerProductList.Count; k++)
            {
                if (buyerProductList[k].quantity!=0)
                {
                    summa += buyerProductList[k].quantity * buyerProductList[k].price;
                    Console.WriteLine($"{k + 1}...{buyerProductList[k].name}");
                }
            }
            Console.WriteLine($"\n Customer: How muth is it?");
            Console.WriteLine($" Seller: Its {summa} grn");

            if (purse>=summa)
            {
                hasMoney = true;
                Console.WriteLine($" Customer: Take it {purse} grn");
                Console.WriteLine($" Seller: Here is your change {purse-summa} grn");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                Console.WriteLine($" Customer: I have only {purse} grn");
                Console.WriteLine(" Seller: You haven't got enought money for buy this list of products");
                Console.ResetColor(); // сбрасываем в стандартный
            }
            return hasMoney;
        }
    }
}
