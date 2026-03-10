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
                _ = HandleClientAsync(client);
            }
        }

        private static void addLogs(string line)
        {
            line = $"[{DateTime.Now:HH:mm}] " + line;
            //File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.), line+'\n' );
            Console.WriteLine(line);
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            int user_id = -1;
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            {
                try
                {
                    while (true)
                    {
                        string request = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(request))
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
                            if(result.StartsWith("SUCCESS"))
                            {
                                user_id = Convert.ToInt32(result.Split('|')[1]);
                                await AuthService.makeStatus(user_id, true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    addLogs(ex.Message);
                }
                finally
                {
                    addLogs($"Client disconnected [{user_id}]");
                    if(user_id != -1)
                    {
                        await AuthService.makeStatus(user_id, false);
                    }
                    client.Close();
                }
            }   
        }
    }
}
