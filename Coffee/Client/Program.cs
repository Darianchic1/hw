using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Client
{
    [Serializable]
    public class Purchase
    {
        [Required]
        [Range(0, 10000)]
        public int id { get; set; }

        [Range(0.01, 10000)] 
        public double price { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string date { get; set; }

        public Purchase(int nid, double nprice, string ndate)
        {
            id = nid;
            price = nprice;
            date = ndate;
        }

        public Purchase()
        {
            // Пустой конструктор
        }
    }

    public class Program
    {
        private const string BaseUrl = "http://localhost";
        private const string Port = "5087";
        private const string AddPurchaseMethod = "/store/add";
        private const string ShowPurchasesMethod = "/store/show";
        private const string DeletePurchasesMethod = "/store/delete";

        private static bool IsAuthorized = false;
        private static readonly HttpClient Client = new HttpClient();

        private static void DisplayPurchases()
        {
            var url = $"{BaseUrl}:{Port}{ShowPurchasesMethod}";

            var response = Client.GetAsync(url).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result; 
            Console.WriteLine(responseContent);
            var purchases = JsonSerializer.Deserialize<List<Purchase>>(responseContent);


            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("| ID покупателя | Стоимость заказа | Дата заказа |");
            Console.WriteLine("-----------------------------------------------------------------");

            foreach (var purchase in purchases)
            {
                Console.WriteLine($"| {purchase.id, -18} | {purchase.price, -5} | {purchase.date, -19} |"); 
            }

            Console.WriteLine("-----------------------------------------------------------------");
        }

        private static void SendPurchase()
        {

            var url = $"{BaseUrl}:{Port}{AddPurchaseMethod}";

            Console.WriteLine("Введите ID заказа:");
            var id = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите цену продукта:");
            var price = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату заказа:");
            var date = Console.ReadLine();

            var purchase = new Purchase(id,price,date); 

            var json = JsonSerializer.Serialize(purchase);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = Client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        private static void DeletePurchase()
        {

            var url = $"{BaseUrl}:{Port}{DeletePurchasesMethod}";

            Console.WriteLine("Введите ID заказа:");
            var id = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите цену продукта:");
            var price = double.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату заказа:");
            var date = Console.ReadLine();

            var purchase = new Purchase(id,price,date); 

            var json = JsonSerializer.Serialize(purchase);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = Client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        //мне сказали, что client не нужно делать, поэтому delete здесь недоделан((


        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1. Добавить заказ");
                Console.WriteLine("2. Удалить заказ по ID");
                Console.WriteLine("3. Вывести все заказы");
                Console.WriteLine("4. Выйти");
                Console.Write("Введите ваш выбор: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SendPurchase();
                        break;

                    case "2":
                        DeletePurchase();
                        break;

                    case "3":
                        DisplayPurchases();
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
