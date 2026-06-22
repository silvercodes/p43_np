

using System.Net;
using System.Net.Sockets;
using System.Text;

const string serverIp = "127.0.0.1";
const int port = 8080;

using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

try
{
    socket.Bind(endpoint);
    socket.Listen();

    Console.WriteLine($"Server started at {serverIp}:{port}");

    Socket remoteSocket = socket.Accept(); // BLOCKING (ожидание установки соединения)
    Console.WriteLine("Connection opened...");

    string message = ReadMessage(remoteSocket);

    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {message}");

    Thread.Sleep(3000);

    string response = "Hello from server!!!";
    remoteSocket.Send(Encoding.UTF8.GetBytes(response));

    remoteSocket.Shutdown(SocketShutdown.Both);
    remoteSocket.Close();

    Console.WriteLine("Connection closed...");
}
catch (Exception ex)
{
    Console.WriteLine($"SERVER ERROR: {ex.Message}");
}

string ReadMessage(Socket remoteSocket)
{
    byte[] buffer = new byte[1024];
    int byteCount = 0;
    string message = string.Empty;

    do
    {
        byteCount = remoteSocket.Receive(buffer);           // BLOCKING
        message += Encoding.UTF8.GetString(buffer, 0, byteCount);

    } while (remoteSocket.Available > 0);

    return message;
}