using System.Collections.Generic;
using System.Data.Entity;

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

    public class Currency
    {
        public int ID { set; get; }
        public string waluta { set; get; }
        public double kurs { set; get; }
        public string dzien { set; get; }
    }

    public class ICurrency : DbContext
    {
        public virtual DbSet<Currency> Waluta { get; set; }
    }
}