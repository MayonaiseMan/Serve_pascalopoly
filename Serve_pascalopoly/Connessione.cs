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
                //get local machine ip
                LocalIp = IPAddress.Parse(GetLocalIPAddress());

                //generate Listener foc tcp connection
                Listener = new TcpListener(LocalIp, LOCAL_PORT);

                _main = main;


            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }

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


                    //waiting for connection
                    TcpClient client = Listener.AcceptTcpClient();
                    string tmp = "client" + (ClientConnessi.Count + 1);
                    ListenToTheStereo(client);
                    ClientConnessi.Add(tmp,client);
                    
                    _main.WriteConsole("nuova connessione con: " + client.Client.LocalEndPoint.ToString());
               }
            });
        }

        public async void ListenToTheStereo(TcpClient client) //yeah i know, i have several mental problem, however it's katekyo hitman reborn opening song n°8 
        {
            await Task.Run(() => {

                
                NetworkStream linea = client.GetStream();
                string data = null;
                int i = 0;
                while (true)
                {
                    try
                    {
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
