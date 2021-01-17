using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_6_laba_sem.Class
{
    class Inf_struct
    {
        public string category;
        public string name;
        public int price;
        public int usefulness;

        public Inf_struct(string _category, string _name, int _price, int _usefulness)
        {
            category = _category;
            name = _name;
            price = _price;
            usefulness = _usefulness;
        }
    }
}

