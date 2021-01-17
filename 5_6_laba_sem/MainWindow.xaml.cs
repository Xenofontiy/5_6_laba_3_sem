using _5_6_laba_sem.Class;
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
            public string _Inf_category { get; set; }
            public string _Inf_name { get; set; }
            public int _Inf_price { get; set; }
            public int _Inf_usefulness { get; set; }
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
                            _Inf_data._Inf_category = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 1)
                        {
                            _Inf_data._Inf_name = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 2)
                        {
                            _Inf_data._Inf_usefulness = Convert.ToInt32(_line.Substring(0, _line.IndexOf(":")));
                            ++numer;
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);


                            _Inf_data._Inf_price = Convert.ToInt32(_line);
                            numer = 0;

                            _line = " ";
                        }
                        if (numer != 0)
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);
                    }
                    var Inf_data = new List<Inf_struct>()
                    {
                       new Inf_struct(_Inf_data._Inf_category, _Inf_data._Inf_name, _Inf_data._Inf_price, _Inf_data._Inf_usefulness)
                    };
                    Food_List_ListView.Items.Add(_Inf_data._Inf_category + ':' + _Inf_data._Inf_name + ':' + _Inf_data._Inf_price + ':' + _Inf_data._Inf_usefulness);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Basket_ListView.ItemsSource = null;
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
                            if (testValue > ic[j].TotalUsefulness) {
                                (ic[j] = lighterCollection.Copy()).AddItem(Inf_data[i], k);
                            }
                        }
                    }
            Basket_ListView.ItemsSource = ic;

        }
    }
}
