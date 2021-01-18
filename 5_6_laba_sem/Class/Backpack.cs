using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_6_laba_sem.Class
{
    class Backpack
    {

        List<Inf_class> Best_Basket = new List<Inf_class>();
        public int Best_Usefulness, Best_Prise, Max_Prise;
        
        public Backpack(int _Max_Prise)
        {
            Max_Prise = _Max_Prise;
        }

        public int CalcPrise(List<Inf_class> Inf_data)
        {
            int Sum_Prise = 0;
            foreach (Inf_class i in Inf_data)
            {
                Sum_Prise += i.price;
            }
            return Sum_Prise;
        }
        public int CalcUsefulness(List<Inf_class> Inf_data)
        {
            int Sum_Usefulness = 0;

            foreach (Inf_class i in Inf_data)
            {
                Sum_Usefulness += i.usefulness;
            }

            return Sum_Usefulness;
        }

        public void CheckSet(List<Inf_class> Inf_data)
        {
            if (Best_Basket == null)
            {
                if (CalcPrise(Inf_data) <= Max_Prise)
                {
                    Best_Basket = Inf_data;
                    Best_Usefulness = CalcUsefulness(Inf_data);
                    Best_Prise = CalcPrise(Inf_data);
                }
            }
            else
            {
                if (CalcPrise(Inf_data) <= Max_Prise && CalcUsefulness(Inf_data) > Best_Usefulness)
                {
                    Best_Basket = Inf_data;
                    Best_Usefulness = CalcUsefulness(Inf_data);
                    Best_Prise = CalcPrise(Inf_data);
                }
            }
        }

        public void MakeAllSets(List<Inf_class> Inf_data)
        {
            if (Inf_data.Count > 0) CheckSet(Inf_data);
            for (int i = 0; i < Inf_data.Count; i++)
            {
                List<Inf_class> newSet = new List<Inf_class>(Inf_data);
                newSet.RemoveAt(i);
                MakeAllSets(newSet);
            }
        }

        public List<Inf_class> GetBestSet()
        {
            return Best_Basket;
        }

    }
}
