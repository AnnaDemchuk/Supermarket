using System;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ShopApp v1.0");
            Shop shop = new Shop();
            shop.Start();
        }
    }
}
