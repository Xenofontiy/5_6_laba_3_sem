using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5_6_laba_sem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Path_inf;
        _Inf_struct _Inf_data = new _Inf_struct();
        Inf__struct Inf_data = new Inf__struct();

        public struct _Inf_struct
        {
            public string category { get; set; }
            public string name { get; set; }
            public int pries { get; set; }
            public int usefulness { get; set; }
        }

        public struct Inf__struct
        {
            public List<_Inf_struct> inf;
        }

        public MainWindow()
        {
            InitializeComponent();
            Inf_data.inf = new List<_Inf_struct>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();              //Открытие окошка где происходит поиск
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Path_inf = dlg.FileName;
            }
            
            int numer = 0;
            using (StreamReader sr = new StreamReader(Path_inf, System.Text.Encoding.Default))  //чтение и разбивка по классам
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
                            _Inf_data.pries = Convert.ToInt32(_line.Substring(0, _line.IndexOf(":")));
                            ++numer;
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);


                            _Inf_data.usefulness = Convert.ToInt32(_line);
                            numer = 0;

                            _line = " ";
                        }
                        if (numer != 0)
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);
                    }
                    Inf_data.inf.Add(_Inf_data);
                    Food_List_ListView.Items.Add(_Inf_data.category + ':' + _Inf_data.name + ':' + _Inf_data.pries + ':' + _Inf_data.usefulness);
                }
            }
        }
    }
}
