namespace App
{
  public class SSLClientDemo
  {
    private readonly IConfiguration? _config;
    private readonly ILogger? _logger;
 
    public SSLClientDemo(IConfiguration config, ILogger logger)
    {
      _config = config;
      _logger = logger;
    }
    
    public async Task Run(string[] args)
    {
      // SSL address
      string address = "127.0.0.1";

      // SSL port
      int port = 5555;

      Console.WriteLine($"SSL address: {address}");
      Console.WriteLine($"SSL port: {port}");

      Console.WriteLine();

      var context = new SslContext(SslProtocols.Tls12, (sender, certificate, chain, sslPolicyErrors) => true);

      // Create a new SSL user client
      var client = new UserClient(context, address, port);

      // Connect the client
      Console.Write("Client connecting...");
      client.ConnectAsync();
      Console.WriteLine("Done!");

      Console.WriteLine("Press Enter to stop the client or '!' to reconnect the client...");

      // Perform text input
      for (; ; )
      {
        string line = Console.ReadLine();
        if (string.IsNullOrEmpty(line))
          break;

        // Disconnect the client
        if (line == "!")
        {
          Console.Write("Client disconnecting...");
          client.DisconnectAsync();
          Console.WriteLine("Done!");
          continue;
        }

        // Send the entered text to the chat server
        client.SendAsync(line);
      }

      // Disconnect the client
      Console.Write("Client disconnecting...");
      client.DisconnectAndStop();
      Console.WriteLine("Done!");
    }
  }
}
