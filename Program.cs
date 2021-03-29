using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace Zad2
{
    class Program
    {
        public void Start()
        {
            Console.WriteLine("============================");
            Console.WriteLine("|     Witaj w programie    |");
            Console.WriteLine("============================");
            Thread.Sleep(1000);
        }

        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("|     Witaj w programie    |");
            Console.WriteLine("============================");


        }

        static void Main()
        {
            DateTime Today = Today.ToString("yyyy-MM-dd");

            string data = "";
            string waluta = "PLN";


            Console.WriteLine("Podaj walute: ");
            waluta = Console.ReadLine();
            Console.WriteLine("Podaj date (rrrr-mm-dd): ");
            data = Console.ReadLine();

            GetAPI(data,waluta);
            Console.Read();
        }
        public static async void GetAPI(string data, string waluta)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://openexchangerates.org/api/historical/" + data + ".json?app_id=984511ca2af843e59f5229a272a16bec");
            Waluta wal = JsonConvert.DeserializeObject<Waluta>(response);
            Console.WriteLine("Waluta: " + waluta + " kurs: " + wal.Rates[waluta] + " Dzien: " + data);
        }
        
    }
}
