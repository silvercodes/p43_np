
using System.Text;

IApplicationLayer appLayer = new CustomApplicationLayer();
ITransportLayer transportLayer = new SuperTransportLayer();
INetworkLayer networkLayer = new CustomNetworkLayer();
ILinkLayer linkLayer = new CustomLinkLayer();

// Отправка
Message message = new Message()
{
    Data = "vasia",
};
Console.WriteLine(message);

Console.WriteLine("===SEND===");
Message m1 = appLayer.DownProcessing(message);
Message m2 = transportLayer.DownProcessing(m1);
Message m3 = networkLayer.DownProcessing(m2);
Message m4 = linkLayer.DownProcessing(m3);
Console.WriteLine(m4);

Console.WriteLine("\n\n===RECEIVE===");
Message receiveM3 = linkLayer.UpProcessing(m4);
Message receiveM2 = networkLayer.UpProcessing(receiveM3);
Message receiveM1 = transportLayer.UpProcessing(receiveM2);
Message receiveMessage = appLayer.UpProcessing(receiveM1);
Console.WriteLine(receiveMessage);




class Message
{
    public List<string> Headers { get; set; } = new List<string>();
    public string Data { get; set; } = "no_data";
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        Headers.ForEach(h =>  sb.Append($"{h} "));

        return $"{sb.ToString()} {Data}";
    }
}
interface ILayer
{
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message UpProcessing(Message message);
    public Message DownProcessing(Message message);
}
interface IApplicationLayer : ILayer
{

}
interface ITransportLayer : ILayer
{

}
interface INetworkLayer : ILayer
{

}
interface ILinkLayer : ILayer
{

}

class CustomApplicationLayer : IApplicationLayer
{
    private const string header = "CustomApplicationLayer_header";
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message DownProcessing(Message message)
    {
        message.Headers.Insert(0, header);

        return message;
    }
    public Message UpProcessing(Message message)
    {
        // Извлекаем (берём) последний элемент
        string header = message.Headers[0];

        // Удаляем его из списка
        message.Headers.RemoveAt(0);

        Console.WriteLine($"CustomApplicationLayer: {header}");

        return message;
    }
}

class CustomTransportLayer : ITransportLayer
{
    private const string header = "CustomTransportLayer_header";
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message DownProcessing(Message message)
    {
        message.Headers.Insert(0, header);

        return message;
    }
    public Message UpProcessing(Message message)
    {
        // Извлекаем (берём) последний элемент
        string header = message.Headers[0];

        // Удаляем его из списка
        message.Headers.RemoveAt(0);

        Console.WriteLine($"CustomTransportLayer: {header}");

        return message;
    }
}
class SuperTransportLayer : ITransportLayer
{
    private const string header = "SuperTransportLayer_header";
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message DownProcessing(Message message)
    {
        message.Headers.Insert(0, header);

        return message;
    }
    public Message UpProcessing(Message message)
    {
        // Извлекаем (берём) последний элемент
        string header = message.Headers[0];

        // Удаляем его из списка
        message.Headers.RemoveAt(0);

        Console.WriteLine($"SuperTransportLayer: {header}");

        return message;
    }
}

class CustomNetworkLayer : INetworkLayer
{
    private const string header = "CustomNetworkLayer_header";
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message DownProcessing(Message message)
    {
        message.Headers.Insert(0, header);

        return message;
    }
    public Message UpProcessing(Message message)
    {
        // Извлекаем (берём) последний элемент
        string header = message.Headers[0];

        // Удаляем его из списка
        message.Headers.RemoveAt(0);

        Console.WriteLine($"CustomNetworkLayer: {header}");

        return message;
    }
}

class CustomLinkLayer : ILinkLayer
{
    private const string header = "CustomLinkLayer_header";
    public ILayer? UpLayer { get; set; }
    public ILayer? DownLayer { get; set; }
    public Message DownProcessing(Message message)
    {
        message.Headers.Insert(0, header);

        return message;
    }
    public Message UpProcessing(Message message)
    {
        // Извлекаем (берём) последний элемент
        string header = message.Headers[0];

        // Удаляем его из списка
        message.Headers.RemoveAt(0);

        Console.WriteLine($"CustomLinkLayer: {header}");

        return message;
    }
}


