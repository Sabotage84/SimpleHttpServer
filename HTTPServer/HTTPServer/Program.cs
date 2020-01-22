using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server(80);

        }

        static void ClientThread(Object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }
    }
}
