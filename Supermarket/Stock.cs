using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Stock
    {
        public List<Product> stockProductList = new List<Product>();

        public Stock()
        {
            stockProductList = new List<Product>();
            // name, price,  weight, daysStored
            stockProductList.Add(new Product("Jogurt", 20, 300, 3));
            stockProductList.Add(new Product("Сroissant", 20, 100, 10));
            stockProductList.Add(new Product("Ice cream", 20, 100, 60));
            stockProductList.Add(new Product("Сhips", 20, 50, 30));

            stockProductList.Add(new Product("Milk", 25, 1000, 3));
            stockProductList.Add(new Product("Сookies", 70, 1000, 10));
            stockProductList.Add(new Product("Sausage", 190, 1000, 15));

            stockProductList.Add(new Product("Pizza", 150, 1500, 1));
            stockProductList.Add(new Product("Watermelon", 60, 6000, 7));
            stockProductList.Add(new Product("Flour", 30, 2000, 60));
        }

        public void PrintStock ()
        {
            Console.WriteLine("--List product in stock---");
            foreach (var item in stockProductList)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Shelf      : {item.numberShelf}");
                Console.WriteLine($"Name       : {item.name}");
                Console.WriteLine($"Price      : {item.price} grn");
                Console.WriteLine($"Quantity   : {item.quantity}");
                //Console.WriteLine($"Weight     : {item.weight} gr");
                Console.WriteLine($"Days stored: {item.daysStored} days");
                Console.WriteLine($"Date       : {item.dateStartStored.ToShortDateString()}");

                Console.WriteLine("------------------------");
            }
            Console.WriteLine("==============================");
        }
    }
}
