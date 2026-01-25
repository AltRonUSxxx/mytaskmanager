using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket
                (
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
                );
            IPEndPoint thisIp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            server.Bind(thisIp);
            server.Listen(10);
            int clientId = 0;
            while (true)
            {
                Socket newClient = server.Accept();
                Thread receiver = new Thread(() => receiverWork(newClient, clientId));
                clientId++;
            }
        }

        private static void receiverWork(Socket client, int id)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = 0;
                string message;
                while (true)
                {
                    bytesReceived = client.Receive(buffer);
                    message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    string[] requests = message.Split(' ');
                    switch(requests[0])
                    {
                        case "01":
                            string username = requests[1];
                            string password = requests[2];
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
