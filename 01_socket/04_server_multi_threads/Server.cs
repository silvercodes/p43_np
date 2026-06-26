using System.Net.Sockets;

namespace _04_server_multi_threads;

internal class Server
{
    private int backlog;
    public string Host { get; set; }
    public int Port { get; set; }
    public Socket ServerSocket { get; private set; }





}
