using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Diagramma_delle_Classi_Monopoli
{
    public class Proprieta : Carta
    {
        private string _colore;
        private int _costo;
        private int _numCase;
        private bool _hotel;

        public Proprieta(string id, string titolo, string desc, ImageSource source, string col, int cost, int numC, bool h)
        {
            ID = id;
            Titolo = titolo;
            Descrizione = desc;
            ImgSource = source;
            Colore = col;
            Costo = cost;
            NumCase = numC;
            Hotel = h;
        }

        public Proprieta(string col, int cost, int numC, bool h)
        {
            Colore = col;
            Costo = cost;
            NumCase = numC;
            Hotel = h;
        }

        public int CostoVendita
        {
            get => Costo/2;
        }

        public string Colore
        {
            get => _colore;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _colore = value;
                }
            }
        }

        public int Costo
        {
            get => _costo;
            set
            {
                if (value >= 0)
                {
                    _costo = value;
                }
            }
        }

        public bool Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
            }
        }

        public int NumCase
        {
            get => default;
            set
            {
                if (value >= 0)
                {
                    _numCase = value;
                }
            }
    }
}