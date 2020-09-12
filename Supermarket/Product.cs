using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Product 
    {
        public string name;
        public int price;
        public int quantity;

        // Вес продуктв в граммах, нужен для полок.
        public int weight;

        // Номер полки - зависит от веса продукта.
        public int numberShelf;

        // Срок годности в днях.
        public int daysStored;

        // День начала хранения.
        public DateTime dateStartStored;

        public Product()
        {
            quantity = 1;
        }
        public Product(string name, int price, int weight, int daysStored)
        {
            this.name = name;
            this.price = price;
            quantity = 1;

            this.weight = weight;
            if (weight > 0 && weight < 500)
            {
                numberShelf = 1;
            }
            else if (weight >= 500 && weight <= 1000)
            {
                numberShelf = 2;
            }
            else if (weight > 1000)
            {
                numberShelf = 3;
            }

            this.daysStored = daysStored;
            this.dateStartStored = DateTime.Now;
        }

        public Product(Product p)
        {
            this.name = p.name;
            this.price = p.price;
            this.quantity = p.quantity;
            this.weight = p.weight;
            this.numberShelf = p.numberShelf;
            this.daysStored = p.daysStored;
            this.dateStartStored = p.dateStartStored;
        }

  
    }
}
