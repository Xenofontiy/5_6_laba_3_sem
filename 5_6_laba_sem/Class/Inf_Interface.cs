using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_6_laba_sem.Class
{
    public interface Inf_Interface
    {
        string category { get; set; }
        string name { get; set; }
        int price { get; set; }
        int usefulness { get; set; }
    }
}
