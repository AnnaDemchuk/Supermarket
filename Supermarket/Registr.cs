using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Registr
    {
        public DateTime day;
        public string name;
        public int quantity;
        public int price;

        public Registr()
        {
           
        }

        public Registr(DateTime day, string name,int quantity, int price)
        {
            this.day = day;
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }

        //public Registr()
        //{
        //    Registr registr = new Registr();
        //}
        //public Registr(DateTime day, Product product)
        //{
        //    this.day = day;
        //    this.product = product;
        //  }
    }
}
