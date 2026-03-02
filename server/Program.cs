using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            addLogs("Server start");
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 2912);
            listener.Start();

            addLogs("=== Server start ===");
            addLogs("Works on 127.0.0.1:2912");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                addLogs("New client");
                //Task.Run(() => HandleClient(client));
                _ = HandleClientAsync(client);
            }
        }

        private static void addLogs(string line)
        {
            line = $"[{DateTime.Now:HH:mm}] " + line;
            //File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), line+'\n' );
            Console.WriteLine(line);
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    while (true)
                    {
                        string request = await reader.ReadLineAsync();
                        if (request is null)
                        {
                            addLogs("Client disconnected");
                            break;
                        }
                        addLogs($"READ: {request}");
                        string[] arguments = request.Split('|');
                        if (request.StartsWith("LOGIN"))
                        {
                            string result = await AuthService.loginAsync(arguments[1], arguments[2]);
                            await writer.WriteLineAsync(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                addLogs(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }
    }
}
