using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Serve_pascalopoly
{
    class Connessione
    {
        MainWindow _main;
        const int LOCAL_PORT = 50000;

        public Connessione(MainWindow main)
        {


            try
            {
                //qui ricavo il mio ip locala grazie a un metodo che utilizza la classe DNS (il metodo si trova in fondo)
                LocalIp = IPAddress.Parse(GetLocalIPAddress());

                //Creu un oggetto Tcp listener che ascolta per una qualunque connessione tcp sulla porta 50000
                Listener = new TcpListener(LocalIp, LOCAL_PORT);

                _main = main;


            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }

        //qui salvo tutti i miei client con come nome Clien + numero ordinato in basa a quando si è connesso
        public Dictionary<string,TcpClient> ClientConnessi
        {
            get;
            private set;
        }

        public async void Start()
        {
            await Task.Run(() => {
                Listener.Start();
                while(true)
                {
                    _main.WriteConsole("In attesa di connessione...");


                    //qui il listener inizia ad ascoltare, il metodo Accept è bloccante e attende l'arrivo di una richiesta di connessione tcp
                    //e accettandola crea un oggetto tcp client 
                    TcpClient client = Listener.AcceptTcpClient();

                    
                    string tmp = "client" + (ClientConnessi.Count + 1);

                    //qui invece facco partire un metodo in async che ascolta sul singolo client
                    ListenToTheStereo(client);

                    //aggiungo il client alla lista di client connessi
                    ClientConnessi.Add(tmp,client);

                    //aggiorno l'elenco sulla finestra
                    _main.AggiornaClientView();
                    //scrivo un aggiornamento in modo testuale
                    _main.WriteConsole("nuova connessione con: " + client.Client.LocalEndPoint.ToString());
               }
            });
        }

        public async void ListenToTheStereo(TcpClient client) //yeah i know, i have several mental problem, however it's katekyo hitman reborn opening song n°8 
        {
            await Task.Run(() => {

                //qui prendo l'oggetto network stream dal client, esso rappresenta la linea su cui vengono scritti i byte
                NetworkStream linea = client.GetStream();

                
                string data = null;
                int i = 0;
                while (true)
                {
                    try
                    {
                        //qui costantement in ascolto creo un buffer ovvero un array di byte e poi se la lettura del network da riscontro positivo(leggo qualcosa)
                        //allora trasformo il buffer in una stringa e la mando al metodo disambiguatore(parte di Poli) che si occuperà di scomporlo e capire il messaggio
                        byte[] buffer = new byte[10000];
                        if ((i = linea.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            data = Encoding.ASCII.GetString(buffer);
                            data = data.ToLower();
                            _main.Disambiguatore(data);
                            
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }

                }


            });
        }



        //qui il metodo sendTo serve a mandare informazione a uno o più client
        //il motivo per cui ha solo modalità Flood o singola è perché gli unici casi di invio a un client sono per aggiornare lo stato della partito(tutti) o per dire al client che ha appena trasmesso
        // che c'è un errore nella sua trasmissione
        public enum metodoSend { flooding, single};
        public void SenToClient(metodoSend metodo,string msg, string nome = "")
        {
            if(metodo == metodoSend.flooding)
            {
                foreach(KeyValuePair<string, TcpClient> pair in ClientConnessi)
                {
                    NetworkStream stream = pair.Value.GetStream();
                    byte[] bytes = Encoding.ASCII.GetBytes(msg);
                    stream.Write(bytes,0,bytes.Length);
                }
            }   
            else if(nome != "" )
            {
                TcpClient client = ClientConnessi[nome];
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.ASCII.GetBytes(msg);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public IPAddress LocalIp
        {
            get;
            private set;
        }

        public TcpListener Listener
        {
            get;
            private set;
        }

        
        


        
        //qui semplicemente usando la classe DNS ricavo il mio ip
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

    
}
