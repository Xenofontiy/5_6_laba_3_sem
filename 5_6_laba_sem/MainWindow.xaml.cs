using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace _5_6_laba_sem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Path_inf; int Max_Prise, quantity;
        _Inf_struct _Inf_data = new _Inf_struct();
        List<Inf_struct> Inf_data = new List<Inf_struct>();



        public MainWindow()
        {
            InitializeComponent();
        }   


        public struct _Inf_struct
        {
            public string category { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public int usefulness { get; set; }
        }

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

        class ItemCollection
        {
            public Dictionary<string, int> Contents = new Dictionary<string, int>();
            public int TotalUsefulness;
            public int TotalPrice;

            public void AddItem(Inf_struct item, int quantity)
            {
                if (Contents.ContainsKey((item.category + item.name))) Contents[(item.category + item.name)] += quantity;
                else Contents[(item.category + item.name)] = quantity;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();              //Открытие окошка где происходит поиск
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Path_inf = dlg.FileName;
                Food_List_ListView.Items.Clear();
            }
            
            int numer = 0;
            using (StreamReader sr = new StreamReader(Path_inf, System.Text.Encoding.Default))      //чтение и разбивка по классам
            {

                string line, _line;
                while ((line = sr.ReadLine()) != null)
                {
                    _line = line;
                    while ((_line.IndexOf(":") > -1))
                    {
                        if (numer == 0)
                        {
                            _Inf_data.category = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 1)
                        {
                            _Inf_data.name = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 2)
                        {
                            _Inf_data.usefulness = Convert.ToInt32(_line.Substring(0, _line.IndexOf(":")));
                            ++numer;
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);


                            _Inf_data.price = Convert.ToInt32(_line);
                            numer = 0;

                            _line = " ";
                        }
                        if (numer != 0)
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);
                    }
                    var Inf_data = new List<Inf_struct>()
                    { 
                       new Inf_struct(_Inf_data.category, _Inf_data.name, _Inf_data.price, _Inf_data.usefulness)
                    };
                    Food_List_ListView.Items.Add(_Inf_data.category + ':' + _Inf_data.name + ':' + _Inf_data.price + ':' + _Inf_data.usefulness);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Basket_ListView.Items.Clear();
            try
            {
                quantity = Convert.ToInt32(quantity_TextBox.Text);
                Max_Prise = Convert.ToInt32(Max_prise_TextBox.Text);
            }
            catch
            {
                MessageBox.Show("Введите корректно числа");
            }

            ItemCollection[] ic = new ItemCollection[quantity + 1];
            for (int i = 0; i <= quantity; i++) 
                ic[i] = new ItemCollection();
            for (int i = 0; i < Inf_data.Count; i++)
                for (int j = quantity; j >= 0; j--)
                    if (j >= Inf_data[i].price)
                    {
                        int _Quantity = Math.Min(quantity, j / Inf_data[i].price);
                        for (int k = 1; k <= _Quantity; k++)
                        {
                            ItemCollection lighterCollection = ic[j - k * Inf_data[i].price];
                            int testValue = lighterCollection.TotalUsefulness + k * Inf_data[i].usefulness;
                            if (testValue > ic[j].TotalUsefulness)
                                (ic[j] = lighterCollection.Copy()).AddItem(Inf_data[i], k);
                        }
                    }
            Basket_ListView.ItemsSource=ic;

        }
    }
}
