using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_6_laba_sem.Class
{
    public class Inf_class:Inf_Interface
    {
        public string Category;
        public string Name;
        public int Price;
        public int Usefulness;

        string _category, _name;
        int _price, _usefulness;

        public Inf_class()
        {
            _category = Category;
            _name = Name;
            _price = Price;
            _usefulness = Usefulness;
        }

        public string category { get => _category; set => _category=value; }
        public string name { get => _name; set => _name = value; }
        public int price { get => _price; set => _price=value; }
        public int usefulness { get => _usefulness; set => _usefulness = value; }
    }
}

