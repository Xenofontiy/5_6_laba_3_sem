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
        string Path_inf; public int quantity, numer_, Sum_Prise, Sum_Usefulness, Best_Prise, Best_Usefulness, Basket;
        _Inf_struct _Inf_data = new _Inf_struct();
        List<Inf_class> Inf_data = new List<Inf_class>();


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
            try
            {
                Food_List_ListView.Items.Clear();
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();              //Открытие окошка где происходит поиск
                dlg.DefaultExt = ".txt";
                dlg.Filter = "txt files (*.txt)|*.txt";
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
                        Inf_data.Add(item: new Inf_class() { category = _Inf_data._Inf_category, name = _Inf_data._Inf_name, price = _Inf_data._Inf_price, usefulness = _Inf_data._Inf_usefulness });
                        ++numer_;
                    }
                    Food_List_ListView.ItemsSource = Inf_data;
                }
            }
            catch { };
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           Basket_ListView.ItemsSource = null;
            try
            {
                int _Sum_Prise=0, _Sum_Usefulness=0;

                Backpack bp = new Backpack(Convert.ToInt32(Max_prise_TextBox.Text));
                bp.MakeAllSets(Inf_data);
                List<Inf_class> solve = bp.GetBestSet();
                Basket_ListView.ItemsSource = solve;

                foreach (Inf_class _i in bp.GetBestSet()) 
                {
                    _Sum_Prise += _i.price;
                    _Sum_Usefulness += _i.usefulness;
                }

                Sum_prise_TextBox.Text = Convert.ToString(_Sum_Prise);
                Sum_Use_TextBox.Text = Convert.ToString(_Sum_Usefulness);
            }
            catch
            {
                if (Food_List_ListView.ItemsSource == null) MessageBox.Show("Укажите текстовый файл");
                else MessageBox.Show("Введите корректно числа");
            }

        }
    }
}
