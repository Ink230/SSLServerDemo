namespace App;

public class UserSession : SslSession
{
  public UserSession(SslServer server) : base(server) { }

  protected override void OnConnected()
  {
    Console.WriteLine($"Chat SSL session with Id {Id} connected! IP: { IP }");
  }

  protected override void OnHandshaked()
  {
    Console.WriteLine($"Chat SSL session with Id {Id} handshaked! IP: { IP }");

    // Send invite message
    string message = "Hello from SSL chat! Please send a message or '!' to disconnect the client!";
    Send(message);
  }

  protected override void OnDisconnected()
  {
    Console.WriteLine($"Chat SSL session with Id {Id} disconnected!");
  }

  protected override void OnReceived(byte[] buffer, long offset, long size)
  {
    string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
    Console.WriteLine("Incoming: " + message);

    // Multicast message to all connected sessions
    Server.Multicast(message);

    // If the buffer starts with '!' the disconnect the current session
    if (message == "!")
      Disconnect();
  }

  protected override void OnError(SocketError error)
  {
    Console.WriteLine($"Chat SSL session caught an error with code {error}");
  }
}
