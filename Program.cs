using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Zad2
{
    class Program
    {
  

        static ICurrency context = new ICurrency();

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
            Console.WriteLine(" |    1) Wybierz walute    (PLN, EUR, JPY, ...)  |");
            Console.WriteLine(" |       (Domyslna data - dzisiaj)               |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    2) Wybierz date      (RRRR-MM-DD)          |");
            Console.WriteLine(" |       (Domyslna waluta - PLN)                 |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    3) Wybierz walute i date                   |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    4) Pokaz baze danych                       |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    5) Wyczysc baze danych                     |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    6) Wyswietl ponownie menu                  |");
            Console.WriteLine(" |-----------------------------------------------|");
            Console.WriteLine(" |    0) # Wyjscie z programu #                  |");
            Console.WriteLine(" #-------=================================-------#\n");
        }

        public static bool showAllDB()
        {         
            var waluty = (from s in context.Waluta select s).ToList<Currency>();
            bool found = false;
            bool wypisalo = false;
            foreach(var st in waluty) //po kazdym
            {
                foreach (var iter in waluty) //po kazdym do ST
                {
                                        if (iter.ID >= st.ID) //doszlismy lub przekroczylismy ST
                        break;
                    else //od 0 do ST
                    {
                        if (st.waluta == iter.waluta && st.dzien == iter.dzien) // jesli st jest tym samym co iter (czyli wiersz w ktorym jestesmy juz sie wczesniej pojawil)
                        {
                            found = true;
                            break;  // to koniec i wybieramy nastepne st
                        }
                    }
                }
                if (!found)
                {
                    Console.WriteLine($" | Waluta: {st.waluta}\t Kurs: " + String.Format("{0:N4}", st.kurs) + $"\t Dzien: {st.dzien}"); // nie pojawil sie to wypisujemy
                    wypisalo = true;
                }
                found = false;
            }
            return wypisalo;
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

                Thread thread = new Thread(() => GetAPI(data, waluta));

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
                    if (!showAllDB())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!! Pusta baza danych !!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
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
                if(wybor == 6)
                {
                    Console.Clear();
                    Menu();
                }
                if(wybor > 6 || wybor < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!! Wybierz poprawnie !!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (wybor == 1 || wybor == 2 || wybor == 3)
                {
                    thread.Start();
                    thread.Join();

                    // GetAPI(data, waluta);
                    // Thread.Sleep(1500);
                }

            }
        }
        
    }
}
