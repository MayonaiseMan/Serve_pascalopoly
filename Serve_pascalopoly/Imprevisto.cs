using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Diagramma_delle_Classi_Monopoli
{
    public class Imprevisto : Carta
    {
        public Imprevisto(string id, string titolo, string desc, ImageSource source)
        {
            ID = id;
            Titolo = titolo;
            Descrizione = desc;
            ImgSource = source;
        }
    }
}