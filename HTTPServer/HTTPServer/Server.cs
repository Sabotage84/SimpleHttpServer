using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Server
    {

        TcpListener tcpListener;

        public Server(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();



            

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                // Создаем поток
                Thread thread = new Thread(new ParameterizedThreadStart(ClientThread));
                // И запускаем этот поток, передавая ему принятого клиента
                thread.Start(client);
            }

            
        }

        ~Server()
        {
            if (tcpListener != null)
                tcpListener.Stop();
        }

        static void ClientThread(Object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }
    }

    
}
