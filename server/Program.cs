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
                        string result;
                        string[] results;
                        switch (arguments[0])
                        {
                            case "LOGIN":
                                addLogs($"LOGIN request [{user_id}]");
                                result = await AuthService.loginAsync(arguments[1], arguments[2]);
                                await writer.WriteLineAsync(result);
                                if (result.StartsWith("SUCCESS"))
                                {
                                    addLogs("+user logined");
                                    user_id = Convert.ToInt32(result.Split('|')[1]);
                                    await AuthService.makeStatus(user_id, true);
                                }
                                break;

                            case "REGISTER":
                                addLogs($"REGISTER request [{user_id}]");
                                result = await AuthService.registerAsync(arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7]);
                                await writer.WriteLineAsync(result);
                                if (result.StartsWith("SUCCESS"))
                                {
                                    addLogs("New student!");
                                }
                                break;

                            case "GET_STUDENTS":
                                addLogs($"GET_STUDENTS request [{user_id}]");
                                results = await AuthService.getStudents();
                                await writer.WriteLineAsync(string.Join("/", results));
                                break;

                            case "REMOVE":
                                addLogs($"REMOVE request [{user_id}]");
                                result = await AuthService.remove(arguments[1]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;

                            case "ADD_GROUP":
                                addLogs($"ADD_GROUP request [{user_id}]");
                                result = await AuthService.add_group(arguments[1]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;

                            case "GET_GROUPS_NAME":
                                addLogs($"GET_GROUPS_NAME request [{user_id}]");
                                result = await AuthService.get_groups_name();
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;

                            case "GET_GROUPS":
                                addLogs($"GET_GROUPS request [{user_id}]");
                                results = await AuthService.getGroups();
                                await writer.WriteLineAsync(string.Join("/", results));
                                break;

                            case "REMOVE_GROUP":
                                addLogs($"REMOVE_GROUP request [{user_id}]");
                                result = await AuthService.remove_group(arguments[1]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "REGISTER_LESSON":
                                addLogs($"REGISTER_LESSON request [{user_id}]");
                                result = await AuthService.register_lesson(arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], arguments[9], user_id);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "GET_LESSONS":
                                addLogs($"GET_LESSONS request [{user_id}]");
                                result = await AuthService.get_lessons();
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "REMOVE_LESSON":
                                addLogs($"REMOVE_LESSON request [{user_id}]");
                                result = await AuthService.remove_lesson(Convert.ToInt32(arguments[1]));
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "GET_FULL_USER":
                                addLogs($"GET_FULL_USER request [{user_id}]");
                                result = await AuthService.get_full_user(arguments[1]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "REDACT_USER":
                                addLogs($"REDACT_USER request [{user_id}]");
                                result = await AuthService.redact_user(arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], arguments[9]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "GET_USERS_ID":
                                addLogs($"GET_USERS_ID request [{user_id}]");
                                result = await AuthService.get_users_id();
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "GET_USER_FIO_USERNAME_GROUPID":
                                addLogs($"GET_USER_FIO_USERNAME_GROUPID request [{user_id}]");
                                result = await AuthService.get_user_fio_username_groupId(arguments[1]);
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                            case "REVERSE_GROUP_ID":
                                addLogs($"REVERSE_GROUP_ID request [{user_id}]");
                                result = await AuthService.reverse_group_id(Convert.ToInt32(arguments[1]), Convert.ToInt32(arguments[2]), arguments[3].Split('/'));
                                addLogs($"ANSWERS: {result}");
                                await writer.WriteLineAsync(result);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    addLogs(ex.ToString());
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
