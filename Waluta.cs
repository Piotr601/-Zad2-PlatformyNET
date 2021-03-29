using System.Collections.Generic;

namespace Zad2
{
    class Waluta
    {
        public string disclaimer { set; get; }
        public string license { set; get; }
        public int timestamp { set; get; }
        public string Base { set; get; }
        public Dictionary<string, double> Rates { set; get; }
    }
}