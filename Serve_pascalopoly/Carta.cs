using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Diagramma_delle_Classi_Monopoli
{
    public abstract class Carta
    {
        private string _id;
        private string _titolo;
        private string _descrizione;
        private ImageSource _imgSource;

        public string Descrizione
        {
            get => _descrizione;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _descrizione = value;
                }
            }
        }

        public string ID
        {
            get => _id;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _id = value;
                }
            }
        }

        public ImageSource ImgSource
        {
            get => _imgSource;
            set
            {
                _imgSource = value;
            }
        }

        public string Titolo
        {
            get => _titolo;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _titolo = value;
                }
            }
        }
    }
}