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
                while (true)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
