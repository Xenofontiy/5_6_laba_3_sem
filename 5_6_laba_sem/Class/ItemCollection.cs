using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_6_laba_sem.Class
{
    class ItemCollection
    {
        public Dictionary<string, int> Contents = new Dictionary<string, int>();
        public int TotalUsefulness;
        public int TotalPrice;

        public void AddItem(Inf_struct item, int quantity)
        {
            if (Contents.ContainsKey((item.category + item.name))) 
                Contents[(item.category + item.name)] += quantity;
            else 
                Contents[(item.category + item.name)] = quantity;
            TotalUsefulness += quantity * item.usefulness;
            TotalPrice += quantity * item.price;
        }

        public ItemCollection Copy()
        {
            var ic = new ItemCollection();
            ic.Contents = new Dictionary<string, int>(this.Contents);
            ic.TotalUsefulness = this.TotalUsefulness;
            ic.TotalPrice = this.TotalPrice;
            return ic;
        }
    }
}
