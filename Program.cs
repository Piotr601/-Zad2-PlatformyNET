using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zad2
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            //string response = await client.GetStringAsync("http://radoslaw.idzikowski.staff.iiar.pwr.wroc.pl/instruction/students.json");
            string response = await client.GetStringAsync("https://openexchangerates.org/api/latest.json?app_id=984511ca2af843e59f5229a272a16bec");
            Console.WriteLine(response);
            //984511ca2af843e59f5229a272a16bec
        }
        
    }
}
