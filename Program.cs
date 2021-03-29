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
            Console.WriteLine("\n #-------=================================-------#");
            Console.WriteLine(" |               Witaj w programie               |");
            Console.WriteLine(" #-------=================================-------#");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public static void Menu()
        {
            Console.WriteLine("\n #-------=================================-------#");
            Console.WriteLine(" |                   M E N U                     |");
            Console.WriteLine(" #-------=================================-------#");
            Console.WriteLine(" |    1) Podaj walute    (PLN, EUR, JPY, ...)    |");
            Console.WriteLine(" |       (Domyslnie PLN)                         |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    2) Podaj date              (rrrr-mm-dd)    |");
            Console.WriteLine(" |       (Domyslnie dzisiaj)                     |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    3) X Pokaz dane X                          |");
            Console.WriteLine(" |       (w przyszlosci do wyswietlania danych)  |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    0) # Wyjscie z programu #                  |");
            Console.WriteLine(" #-------=================================-------#\n");
        }

        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            Start();
            Menu();

            DateTime Today = DateTime.Now;

            string data = Today.ToString("yyyy-MM-dd");
            string waluta = "PLN";

            var wybor = 99;

            while (wybor != 0)
            {
                Console.Write("\n >> Wybor: ");
                try 
                { 
                    wybor = int.Parse(Console.ReadLine()); 
                }
                catch (Exception e)
                {
                    // wybierz poprawnie
                }

                if (wybor == 1)
                {
                    try
                    {
                        Console.Write("  > Podaj walute: ");
                        waluta = Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!! Podaj poprawna walute !!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                if (wybor == 2)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("  > Podaj date (rrrr-mm-dd): ");
                            data = Console.ReadLine();

                            DateTime tempdate;
                            if (DateTime.TryParse(data, out tempdate))
                            {
                                if (tempdate <= Today)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("!! Tego dnia jeszcze nie by³o ;) !!\n!! Podaj poprawna date (rrrr-mm-dd) !!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("!! Podaj poprawna date (rrrr-mm-dd) !!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! Podaj poprawna date !!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }


                }
                if (wybor == 1 || wybor == 2 || wybor == 3)
                {
                    GetAPI(data,waluta);
                    Thread.Sleep(1500);
                }

                if( wybor != 1 && wybor != 2 && wybor != 3 && wybor != 0 )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!! Wybierz poprawnie !!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                

            }
        }

        public static async void GetAPI(string data, string waluta)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://openexchangerates.org/api/historical/" + data + ".json?app_id=984511ca2af843e59f5229a272a16bec");
            
            try
            {
                Waluta wal = JsonConvert.DeserializeObject<Waluta>(response);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n USD/" + waluta + " kurs: " + wal.Rates[waluta] + " | Dzien: " + data);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!! Blad, Wpisz poprawne dane !!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
    }
}
