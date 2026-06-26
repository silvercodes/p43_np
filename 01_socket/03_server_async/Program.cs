


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

    await Console.Out.WriteLineAsync($"Server started at {serverIp}:{port}");

    Socket remoteSocket = await socket.AcceptAsync();
    await Console.Out.WriteLineAsync("Connection opened...");

    string message = await ReadMessageAsync(remoteSocket);

    await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()}: {message}");

    // Thread.Sleep(3000);                          // BLOCKING
    // new Task(() => Thread.Sleep(3000)).Wait();      // BLOCKING

    // await (new Task(() => Thread.Sleep(3000)));         // :-|
    // >>> EQUALS <<<
    await Task.Delay(3000);                             // :-)))

    string response = "Hello from server!!!";
    await remoteSocket.SendAsync(Encoding.UTF8.GetBytes(response));

    remoteSocket.Shutdown(SocketShutdown.Both);
    remoteSocket.Close();

    await Console.Out.WriteLineAsync("Connection closed...");
}
catch (Exception ex)
{
    await Console.Out.WriteLineAsync($"SERVER ERROR: {ex.Message}");
}

async Task<string> ReadMessageAsync(Socket remoteSocket)
{
    byte[] buffer = new byte[1024];
    int byteCount = 0;
    string message = string.Empty;

    do
    {
        byteCount = await remoteSocket.ReceiveAsync(buffer);
        message += Encoding.UTF8.GetString(buffer, 0, byteCount);

    } while (remoteSocket.Available > 0);

    return message;
}


