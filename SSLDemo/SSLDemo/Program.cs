try
{
  IConfiguration Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"AppSettings/appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"AppSettings/appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

  Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .CreateLogger();

  Log.Information("Initializing services...");

  IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
      services.AddSingleton(Log.Logger);
      services.AddSingleton(Configuration);
      services.AddTransient<SSLServerDemo>();
    })
    .Build();

  Log.Information("Application starting...");

  //Start the app entry point
  await (host?.Services?.GetService<SSLServerDemo>()?.Run(args) ?? throw new Exception("Invalid service initialization."));

  Log.Information("Application closing...");
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
  Log.CloseAndFlush();
}
