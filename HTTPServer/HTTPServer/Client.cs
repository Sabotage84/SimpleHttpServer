using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Client
    {

        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient Client)
        {
            int Count;
            byte[] Buffer = new byte[1024];
            string ContentType = "text/html";

            FileStream FS;
            try
            {
                FS = new FileStream(@"D:\test.html", FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                // Если случилась ошибка, посылаем клиенту ошибку 500
                Console.WriteLine("Test not found");
                return;
            }

            // Посылаем заголовки
            string Headers = "HTTP/1.1 200 OK\nContent-Type: " + ContentType + "\nContent-Length: " + FS.Length + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);

            // Пока не достигнут конец файла
            while (FS.Position < FS.Length)
            {
                // Читаем данные из файла
                Count = FS.Read(Buffer, 0, Buffer.Length);
                // И передаем их клиенту
                Client.GetStream().Write(Buffer, 0, Count);
            }

            // Закроем файл и соединение
            FS.Close();
            Client.Close();
        }
    }
}
