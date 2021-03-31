using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Data.Entity;
using System.Linq;

namespace Zad2
{
    class Program
    {

        static ICurrency context = new ICurrency();
        // METHODS
        /*
         * 
         * 
         */

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
            Console.WriteLine(" |       (Domyslnie dzisiaj)                     |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    2) Podaj date      (RRRR-MM-DD)            |");
            Console.WriteLine(" |       (Domyslnie PLN)                         |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    3) Podaj walute i date                     |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    4) Pokaz baze danych                       |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    5) Wyczysc baze danych                     |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    0) # Wyjscie z programu #                  |");
            Console.WriteLine(" #-------=================================-------#\n");
        }

        public static void showAllDB()
        {
            var waluty = (from s in context.Waluta select s).ToList<Currency>();
            foreach (var st in waluty)
            {
                Console.WriteLine($"ID: {st.ID}, Waluta: {st.waluta}, Kurs: {st.kurs}, Dzien: {st.dzien}");
            }
        }

        public static async void GetAPI(string _data, string _waluta)
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://openexchangerates.org/api/historical/" + _data + ".json?app_id=984511ca2af843e59f5229a272a16bec");

            try
            {
                API_Waluty wal = JsonConvert.DeserializeObject<API_Waluty>(response);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n USD/" + _waluta + " kurs: " + wal.Rates[_waluta] + " | Dzien: " + _data);
                Console.ForegroundColor = ConsoleColor.White;

                // dodaj do bazy danych
                context.Waluta.Add(new Currency { waluta = _waluta, kurs = wal.Rates[_waluta], dzien = _data });
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!! Blad, Wpisz poprawne dane !!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            Start();
            Menu();

            DateTime Today = DateTime.Now;
            string waluta = "PLN";

            var wybor = 99;

            while (wybor != 0)
            {
                string data = Today.ToString("yyyy-MM-dd");
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
                if (wybor == 3)
                {
                    bool flag = true;
                    while(flag)
                    {
                        try
                        {
                            Console.Write("  > Podaj walute: ");
                            waluta = Console.ReadLine();
                            flag = false;
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("!! Podaj poprawna walute !!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }

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
                if(wybor==4)
                {
                    showAllDB();
                }
                if(wybor==5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!! Wyczyszczono baze danych !!");
                    Console.ForegroundColor = ConsoleColor.White;
                    //czyszczenie bazy danych
                    var records = from m in context.Waluta select m;
                    foreach (var record in records)
                        context.Waluta.Remove(record);
                    context.SaveChanges();
                }
                if(wybor>5 || wybor < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!! Wybierz poprawnie !!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (wybor == 1 || wybor == 2 || wybor == 3)
                {
                    GetAPI(data, waluta);
                    Thread.Sleep(1500);
                }

            }
        }
        
    }
}
