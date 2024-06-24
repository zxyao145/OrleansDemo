using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

var host = Host.CreateDefaultBuilder(args);
host.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering()
            .AddMemoryGrainStorageAsDefault()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "OrleansBasics";
            })
            ;
    })
    .UseConsoleLifetime()
    .ConfigureLogging((logging) => logging.AddConsole());

await host.RunConsoleAsync();
