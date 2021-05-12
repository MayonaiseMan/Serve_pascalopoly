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
        private Dictionary<string, string> clientGiocatore;
        public MainWindow()
        {
            InitializeComponent();




        }
        private List<string> log;

        public void Aggiorna(string[] cmd)
        {
            string[] tmp;
            foreach (string s in cmd)
            {
                log.Add(s);
                tmp = s.Split('-');
                switch (tmp[0])
                {
                    case "tiro":
                        SpostaPedina(tmp[1], Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[3]), tmp[4]);
                        break;
                }
            }
        }
        public void SpostaPedina(string nickname, int dado1, int dado2, string doppio)
        {

        }
        public void Disambiguatore(string vs)
        {
            string[] tmp;
            tmp = vs.Split('|');
            switch (tmp[0])
            {
                case "aggiorna":
                    Aggiorna(tmp);

                    break;
            }
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
