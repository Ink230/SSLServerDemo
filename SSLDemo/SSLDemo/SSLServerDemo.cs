namespace App
{
  public class SSLServerDemo
  {
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public SSLServerDemo(IConfiguration config, ILogger logger)
    {
      _config = config;
      _logger = logger;
    }
    public async Task Run(string[] args)
    {
      // SSL port
      int port = 5555;
      if (args.Length > 0)
        port = int.Parse(args[0]);

      Console.WriteLine($"SSL port: {port}");

      Console.WriteLine();
      
      // Create and prepare a new SSL server context
      var context = new SslContext(SslProtocols.Tls12, new X509Certificate2("server.pfx", "password"));

      // Create a new SSL chat server
      var server = new AppServer(context, IPAddress.Any, port);

      // Start the server
      Console.Write("Server starting...");
      server.Start();
      Console.WriteLine("Done!");

      Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

      // Perform text input
      for (; ; )
      {
        string line = Console.ReadLine();
        if (string.IsNullOrEmpty(line))
          break;

        // Restart the server
        if (line == "!")
        {
          Console.Write("Server restarting...");
          server.Restart();
          Console.WriteLine("Done!");
          continue;
        }

        // Multicast admin message to all sessions
        line = "(admin) " + line;
        server.Multicast(line);
      }

      // Stop the server
      Console.Write("Server stopping...");
      server.Stop();
      Console.WriteLine("Done!");
    }
  }
}
