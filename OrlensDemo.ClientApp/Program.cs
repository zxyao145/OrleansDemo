using Orleans.Configuration;
using Orleans;
using OrlensDemo.GrainInterfacesLib;
using Microsoft.Extensions.Logging;

try
{
    using (var client = await ConnectClientAsync())
    {
        for (int i = 0; i < 10; i++)
        {
            var device = client.GetGrain<IDeviceGrain>("did-" + i);
            var response = await device.GetState();
            Console.WriteLine($"{response.IsOnLine}");
        }
      
        Console.ReadKey();
    }

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

static async Task<IClusterClient> ConnectClientAsync()
{
    var client = new ClientBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "OrleansBasics";
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

    await client.Connect();
    Console.WriteLine("Client successfully connected to silo host \n");

    return client;
}