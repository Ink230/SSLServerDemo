namespace App;

public class AppServer : SslServer
{
  public AppServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }

  protected override SslSession CreateSession() { return new UserSession(this); }

  protected override void OnError(SocketError error)
  {
    Console.WriteLine($"Chat SSL server caught an error with code:\n {error}");
  }
}
