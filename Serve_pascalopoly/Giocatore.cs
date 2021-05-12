using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Diagramma_delle_Classi_Monopoli
{
    public class Giocatore
    {
        private int _denaro;
        private List<Proprieta> _proprieta;
        private List<Probabilita> _probabilita;
        private List<Imprevisto> _imprevisti;
        private ImageSource _pedina;
        private string _nickname;
        private bool _sesso;
        private int _eta;

        public Giocatore(string nickname, bool sesso, int eta)
        {

            Nickname = nickname;
            Sesso = sesso;
            Eta = eta;

        }

        public int Denaro
        {
            get
            {
                return _denaro;
            }
            set
            {
                if(value < 0)
                {
                    throw new Exception("CLIENT: Errore, la quantita di denaro non e valida.");
                }
            }
        }

        public int Eta
        {
            get
            {
                return _eta;
            }
            set
            {
                if(value > 0)
                {
                    _eta = value;
                }
                else
                {
                    throw new Exception("CLIENT: Errore, la eta inserita non è valida.");
                }
            }
        }

        public List<Imprevisto> Imprevisti
        {
            get
            {
                return _imprevisti;
            }
            set
            {
                if(value.Count() <= 0)
                {
                    throw new Exception("CLIENT: Errore nella quantita di imprevisti.");
                }
                else
                {
                    _imprevisti = value;
                }
            }
        }

        public string Nickname
        {
            get
            {
                return _nickname;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _nickname = value;
                }
                else
                {
                    throw new Exception("CLIENT: Errore, il nickname non e valido.");
                }
            }
        }

        public ImageSource Pedina
        {
            get
            {
                return _pedina;
            }
            set
            {
                _pedina = value;
            }
        }

        public List<Probabilita> Probabilita
        {
            get
            {
                return _probabilita;
            }
            set
            {
                if(value.Count() <= 0)
                {
                    throw new Exception("CLIENT: Errore nella quantita di probabilita.");
                }
                else
                {
                    _probabilita = value;
                }
            }
        }

        public List<Proprieta> Proprieta
        {
            get
            {
                return _proprieta;
            }
            set
            {
                if(value.Count() <= 0)
                {
                    throw new Exception("CLIENT: Errore nella quantita di proprieta.");
                }
            }
        }

        public bool Sesso
        {
            get
            {
                return _sesso;
            }
            set
            {
                _sesso = value;
            }
        }

        public void AggiungiProprieta(Proprieta proprieta)
        {
            foreach(Proprieta p in Proprieta)
            {
                if(proprieta.ID == p.ID)
                {
                    throw new Exception("La proprietà è già presente");
                }
            }
            Proprieta.Add(proprieta);
        }
    }
}