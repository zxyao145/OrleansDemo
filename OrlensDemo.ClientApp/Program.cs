using Orleans.Configuration;
using Orleans;
using OrlensDemo.GrainInterfacesLib;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

try
{
    var host = CreateHost();
    Console.WriteLine("Connecting to server \n");
    await host.StartAsync();
    Console.WriteLine("Connected \n");

    var client = host.Services.GetRequiredService<IClusterClient>();
    for (int i = 0; i < 10; i++)
    {
        var device = client.GetGrain<IDeviceGrain>("did-" + i);
        var response = await device.GetState();
        Console.WriteLine($"{response.IsOnLine}");
    }

    Console.ReadKey();

    return 0;
}
catch (Exception e)
{
    Console.WriteLine($"\nException while trying to run client: {e.Message}");
    Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
    Console.WriteLine("\nPress any key to exit.");
    Console.ReadKey();
    return 1;
}


static IHost CreateHost()
{
    var services = new ServiceCollection();
    var host = new HostBuilder()
        .UseOrleansClient(clientBuilder =>
        {
            clientBuilder.UseLocalhostClustering();

            clientBuilder.Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "OrleansBasics";
            });
        })
        .UseConsoleLifetime()
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();
    return host;

}
