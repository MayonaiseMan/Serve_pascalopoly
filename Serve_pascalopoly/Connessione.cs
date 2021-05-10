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
                    ClientConnessi.Add(tmp,client);
                    
                    _main.WriteConsole("nuova connessione con: " + client.Client.LocalEndPoint.ToString());
               }
            });
        }

        public async void ListenToTheStereo(TcpClient client) //yeah i know, i have several mental problem
        {
            await Task.Run(() => {
                byte[] buffer;    
                
            
            });
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

        
        


        public byte[] GetBufferHead()
        {
            if(Buffer.Count > 0)
            {
                byte[] vs = Buffer.Dequeue();
                return vs;
            }
            else
            {
                return new byte[0];
            }
            

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
