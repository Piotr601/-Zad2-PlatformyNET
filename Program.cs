using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace Zad2
{
    class Program
    {
        public static void Start()
        {
            Console.WriteLine("===================================");
            Console.WriteLine("|     Witaj w programie           |");
            Console.WriteLine("===================================");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public static void Menu()
        {

            Console.WriteLine("===================================");
            Console.WriteLine("|    1) Podaj walute              |");
            Console.WriteLine("|      (Domyslnie PLN)            |");
            Console.WriteLine("|    2) Podaj date                |");
            Console.WriteLine("|      (Domyslnie dzisiaj)        |");
            Console.WriteLine("|    3) Pokaz dane                |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("|    0) Wyjscie z programu        |");
            Console.WriteLine("===================================");

        }

        static void Main()
        {

            //Start();

            //Menu();

            DateTime Today = DateTime.Now;

            string data = Today.ToString("yyyy-MM-dd");
            string waluta = "PLN";

            var wybor = 99;

            while (wybor != 0)
            {
                Console.Write(" >> Wybor: ");
                wybor = int.Parse(Console.ReadLine());

                if (wybor == 1)
                {
                    try
                    {
                        Console.Write("Podaj walute: ");
                        waluta = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("!! Podaj poprawna walute !!");
                    }
                }

                if (wybor == 2)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("Podaj date (rrrr-mm-dd): ");
                            data = Console.ReadLine();

                            DateTime tempdate;
                            if (DateTime.TryParse(data, out tempdate))
                            {
                                if (tempdate <= Today)
                                {
                                    break;
                                }
                                else
                                    Console.WriteLine("!! Podaj poprawna date (rrrr-mm-dd) !!");
                            }
                            else
                            {
                                Console.WriteLine("!! Podaj poprawna date (rrrr-mm-dd) !!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("!! Podaj poprawna date !!");
                        }
                    }


                }
                if (wybor == 1 || wybor == 2 || wybor == 3)
                {
                    GetAPI(data,waluta);
                    Thread.Sleep(1500);
                }

            }

            Console.Read();
        }

        public static async void GetAPI(string data, string waluta)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://openexchangerates.org/api/historical/" + data + ".json?app_id=984511ca2af843e59f5229a272a16bec");
            
            try
            {
                Waluta wal = JsonConvert.DeserializeObject<Waluta>(response);
                Console.WriteLine("                          USD/" + waluta + " kurs: " + wal.Rates[waluta] + " | Dzien: " + data);
            }
            catch(Exception e)
            {
                Console.WriteLine("!! Blad, Wpisz poprawne dane !!");
            }
        }
        
    }
}
