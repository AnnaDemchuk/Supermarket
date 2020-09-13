using System;
using System.Collections.Generic;
using System.Text;

namespace Supermarket
{
    public class Shop
    {
        public List<Product> productListShop = new List<Product>();
        public List<Buyer> buyerList = new List<Buyer>();
        public Stock stock = new Stock();
        public Statistics statistics = new Statistics();
        public DateTime dateInShop; // 2 клиента = 1 день

        public void Start()
        {
            DataAdd(0);// Установить/сменить дату дня в магазине
            FirstDelivery(); // Загрузить продукты со склада
            PanelManager(); // Меню
        }


        public void ShowDay()
        {
            Console.ForegroundColor = ConsoleColor.Magenta; // Устанавливаем цвет
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine($" Welcome to our supermarket! ");
            Console.WriteLine($" Today is - {dateInShop.ToShortDateString()}  ");
            Console.WriteLine("---------------------------------------------");
            Console.ResetColor(); // Сбрасываем в стандартный
        }

        public void FirstDelivery()
        {
            ShowDay();
            Random random = new Random();
            // Загрузка со склада.
            for (int i = 0; i < stock.stockProductList.Count; i++)
            {
                productListShop.Add(new Product(stock.stockProductList[i]));
                productListShop[i].quantity = random.Next(3, 5);
            }
            Console.WriteLine();
        }

        public void PanelManager()
        {
            PrintShopProduct();

            int choise;
            for (; ; )
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("  MENU  ");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Press - 1, if you want to sell product for 1 buyer");
                Console.WriteLine("Press - 2, if you want to sell product queue buyer");
                Console.WriteLine("Press - 3, if you want to print statistic for all days");
                Console.WriteLine("Press - 4, if you want to print statistic for 1 days");
                Console.WriteLine("Press - 5, if you want to print product catalog shop");
                Console.WriteLine("Press - 6, if you want to exit to admin panel");
                Console.WriteLine("Press - 7, if you want to print stok");
                Console.WriteLine("Press - 8, if you want to print  BuyersList");
                Console.WriteLine("--------------------------------------------------");
                if (int.TryParse(Console.ReadLine(), out choise) == true)
                {
                    if (choise == 1)
                    {
                        CreateBuyer();
                    }
                    else if (choise == 2)
                    {
                        CreateBuyersQueue();
                    }
                    else if (choise == 3)
                    {
                        statistics.PrintAllStatistic();
                    }
                    else if (choise == 4)
                    {
                        statistics.AskStatisticDay();
                    }

                    else if (choise == 5)
                    {
                        PrintShopProduct();
                    }

                    else if (choise == 6)
                    {
                        break;
                    }
                    else if (choise == 7)
                    {
                        stock.PrintStock();
                    }
                    else if (choise == 8)
                    {
                        PrintBuyersList();
                    }

                    else
                    {
                        Console.WriteLine("Incorrect input");
                    }
                }
            }
        }

        public void DataAdd(int change)
        {
            if (change == 0)
            {
                dateInShop = DateTime.Now;
            }
            else
            {
                dateInShop = dateInShop.AddDays(1);
            }
        }
        public void PrintShopProduct()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Catalog products of supermarket  \n");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"__________Shelf {i + 1}_________");
                foreach (var item in productListShop)
                {
                    if (item.numberShelf == i + 1)
                    {
                        item.PrintProduct();
                    }
                }
            }
            Console.WriteLine("_____________________________");
            Console.ResetColor();
        }


        public Buyer CreateListProductForOneBuyer()
        {
            Buyer buyer = new Buyer();
            int myProductList = 3;// В списке у каждого покупателя 3 товара
            Random random = new Random();
            int index;
            for (int i = 0; i < myProductList; i++)
            {
                index = random.Next(0, stock.stockProductList.Count);
                buyer.buyerProductList.Add(new Product(stock.stockProductList[index]));///изм.1
            }
            return buyer;
        }

        public void CreateBuyersQueue()
        {
            int queue;
            for (; ; )
            {
                Console.WriteLine("Enter amount people in the queue");
                if (int.TryParse(Console.ReadLine(), out queue))
                {
                    if (queue >= 2 && queue < 20)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input (queue 2-20 buyers)");
                    }
                }
            }

            Buyer buyer = new Buyer();
            for (int i = 0; i < queue; i++)
            {
                buyer = CreateListProductForOneBuyer();
                BuyProduct(buyer);
            }
        }

        public void CreateBuyer()
        {
            Buyer buyer = new Buyer();
            buyer = CreateListProductForOneBuyer(); // Заполнение продуктами списка 
            BuyProduct(buyer);
        }

        //процесс покупки общий для всех что для 1покупателя  что для очереди
        public void BuyProduct(Buyer buyer)
        {
            {
                // все продукты из списка покупателей есть в магазине
                if (EmptyReceipt(buyer) == false)
                {
                    //проверка хватит ли денег у покупателя
                    if (buyer.CheckSumma() == true)
                    {
                        DeleteProduct(buyer);
                        AddStatistic(buyer);
                        EmptyShop();
                        buyer.GenerationCheck();

                        buyerList.Add(buyer);
                    }

                    // добавление +1 день если прошло 2 покупателя    
                    if (buyerList.Count % 2 == 0 && buyerList.Count != 0)
                    {
                        DataAdd(1);
                        ShowDay();
                        Inventory();
                    }
                }
            }
        }

        public bool EmptyReceipt(Buyer buyer)
        {
            int count = 0;
            bool emptyReceipt = false;
            for (int i = 0; i < productListShop.Count; i++)
            {
                for (int j = 0; j < buyer.buyerProductList.Count; j++)
                {
                    if (productListShop[i].name == buyer.buyerProductList[j].name)
                    {
                        if (productListShop[i].quantity == 0)
                        {
                            // если нет в магазине -обнуляем количество этого товара у покупателя  в списке
                            //  productListShop.Remove(productListShop[i]);
                            buyer.buyerProductList[j].quantity = 0;
                            Console.WriteLine($"Seller: Sorry...we dont have -{buyer.buyerProductList[j].name}");
                            count++;
                        }
                    }
                }
            }
            if (count == 3)
            {
                emptyReceipt = true;
                Console.WriteLine("Seller: Sorry...we dont have all list of product\n");
            }
            return emptyReceipt;
        }


        public void DeleteProduct(Buyer buyer)
        {
            Console.WriteLine("\n-----we sell-----");
            for (int i = 0; i < productListShop.Count; i++)
            {
                for (int j = 0; j < buyer.buyerProductList.Count; j++)
                {
                    if (productListShop[i].name == buyer.buyerProductList[j].name && productListShop[i].quantity != 0 && buyer.buyerProductList[j].quantity != 0)
                    {
                        productListShop[i].quantity = productListShop[i].quantity - buyer.buyerProductList[j].quantity;
                        Console.WriteLine($"We sell product -{productListShop[i].name}");
                    }
                    else if (productListShop[i].name == buyer.buyerProductList[j].name && productListShop[i].quantity == 0 && buyer.buyerProductList[j].quantity != 0)
                    {
                        buyer.buyerProductList[j].quantity = 0;
                        //товар 1 штука в магазине, в у клиента 2 или 3 одинаковых позиции
                    }
                    else if (productListShop[i].quantity == 0)
                    {
                        //если списался последний продукт этой категории
                        productListShop[i].daysStored = 0; // обнулить срок хранения
                        productListShop[i].dateStartStored = new DateTime(2000, 1, 1); //обнулить дату изготовления
                    }
                }
            }
            Console.WriteLine("===========================");
        }


        public void AddStatistic(Buyer buyer)
        {
            Registr registr = new Registr();

            for (int i = 0; i < buyer.buyerProductList.Count; i++)
            {
                registr.day = dateInShop;
                registr.name = buyer.buyerProductList[i].name;
                registr.quantity = 1;
                registr.price = buyer.buyerProductList[i].price;
                statistics.statisticList.Add(registr);
            }
        }

        public void Inventory()
        {
            bool emptyList = true;
            Console.WriteLine("----------------------");
            Console.WriteLine($"Inventory report {dateInShop.ToShortDateString()} \n");
            for (int i = 0; i < productListShop.Count; i++)
            {
                if (productListShop[i].dateStartStored.AddDays(productListShop[i].daysStored) <= dateInShop && productListShop[i].daysStored != 0)
                {
                    Console.WriteLine($"We delete { productListShop[i].name}" +
                        $"_made___{productListShop[i].dateStartStored.ToShortDateString()}" +
                        $"_the shelf life__{productListShop[i].daysStored} days");
                    productListShop[i].quantity = 0;// удаляем продукт


                    productListShop[i].daysStored = 0; // обнулить срок хранения
                    productListShop[i].dateStartStored = new DateTime(2000, 1, 1); //обнулить дату изготовления
                    emptyList = false;
                }
            }
            if (emptyList==true)
            {
                Console.WriteLine($"\nToday all products are fresh (this day without invetory)");
            }
            Console.WriteLine("----------------------");
        }

        public void EmptyShop()
        {
            int count = 0;
            for (int i = 0; i < productListShop.Count; i++)
            {
                count += productListShop[i].quantity;
            }

            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                Console.WriteLine($"\nWe sell all products");
                Console.WriteLine($"Start delivery from stock\n");

                productListShop.Clear();
                FirstDelivery();
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Shop not empty - left {count} product\n");
            }
        }

        public void PrintBuyersList()
        {
            for (int i = 0; i < buyerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Buyer");
                buyerList[i].PrintBuyers();
                Console.WriteLine("");
            }
        }
    }
}
