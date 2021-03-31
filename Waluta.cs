using System.Collections.Generic;

namespace Zad2
{
    class API_Waluty
    {
        public string disclaimer { set; get; }
        public string license { set; get; }
        public int timestamp { set; get; }
        public string Base { set; get; }
        public Dictionary<string, double> Rates { set; get; }
    }

    public class Historia
    {
        public int ID { set; get; }
        public string waluta { set; get; }
        public string kurs { set; get; }
        public string dzien { set; get; }
    }

    public class IHistoria : Historia
    {

    }
}