using System;
using System.Collections.Generic;
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

namespace Serve_pascalopoly
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();




        }

        public void Disambiguatore(string vs)
        {
            //TODO: roba
        }

        public void CheckConsole()
        {
            string s = "";
            if (out_lbl.Content != null)
            {
                s = (string)out_lbl.Content;
                string[] vs = s.Split('\n');
                if (vs.Length >= 10)
                {
                    out_lbl.Content = "";
                }
            }
        }

        public void WriteConsole(string s)
        {
            CheckConsole();
            out_lbl.Content += s + "\n";
        }
    }
}
