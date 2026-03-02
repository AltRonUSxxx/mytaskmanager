using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ServerConnection
{
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;


    public async Task ConnectAsync()
    {
        _client = new TcpClient();
        await _client.ConnectAsync("127.0.0.1", 2912);
        var stream = _client.GetStream();
        _reader = new StreamReader(stream);
        _writer = new StreamWriter(stream) { AutoFlush = true };
    }

    public void Connect()
    {
        _client = new TcpClient("127.0.0.1", 2912);
        var stream = _client.GetStream();
        _reader = new StreamReader(stream);
        _writer = new StreamWriter(stream) { AutoFlush = true };
    }

    public string Send(string message)
    {
        _writer.WriteLine(message);
        return _reader.ReadLine();
    }

    public async Task<string> SendAsync(string message)
    {
        await _writer.WriteLineAsync(message);
        return await _reader.ReadLineAsync();
    }

    public void Close()
    {
        _reader.Close();
        _writer.Close();
        _client.Close();
    }
}
