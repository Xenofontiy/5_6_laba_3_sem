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
        public struct _Inf_struct
        {
            public string category { get; set; }
            public string name { get; set; }
            public int pries { get; set; }
            public int usefulness { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();


            int numer = 0;
            string Path_inf = @"C:\Users\Xeno\Desktop\C#\4_laba_3_sem\4_laba_3_sem\Data.txt"; //забор инфы с файла
            using (StreamReader sr = new StreamReader(Path_inf, System.Text.Encoding.Default))

            {
                _Inf_struct Inf_data = new _Inf_struct();
                string line, _line;
                while ((line = sr.ReadLine()) != null)
                {
                    _line = line;
                    while ((_line.IndexOf(":") > -1))
                    {
                        if (numer == 0)
                        {
                            Inf_data.category = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 1)
                        {
                            Inf_data.name = _line.Substring(0, _line.IndexOf(":"));
                            ++numer;
                        }
                        else if (numer == 2)
                        {
                            Inf_data.pries = Convert.ToInt32(_line.Substring(0, _line.IndexOf(":")));
                            ++numer;
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);


                            Inf_data.usefulness = Convert.ToInt32(_line);
                            numer = 0;

                            _line = " ";
                        }
                        if (numer != 0)
                            _line = _line.Remove(0, (_line.IndexOf(":")) + 1);
                    }
                    Console.WriteLine(Inf_data.category + ':' + Inf_data.name + ':' + Inf_data.pries + ':' + Inf_data.usefulness);
                    Console.ReadLine();
                }
            }
        }

            

        
    
    }
}
