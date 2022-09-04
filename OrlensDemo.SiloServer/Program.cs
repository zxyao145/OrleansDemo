using Orleans.Configuration;
using Orleans.Hosting;
using OrlensDemo.GrainsLib;
using Orleans;
using System.Net;

//https://docs.microsoft.com/zh-cn/dotnet/orleans/tutorials-and-samples/tutorial-1

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseOrleans(c =>
{
    c.UseLocalhostClustering()
    .AddMemoryGrainStorageAsDefault()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "OrleansBasics";
    })
     //.Configure<EndpointOptions>(
     //                       options => options.AdvertisedIPAddress = IPAddress.Loopback)

    .ConfigureApplicationParts(parts =>
        parts.AddApplicationPart(typeof(DeviceGrain).Assembly)
        .WithReferences()
    )
    .ConfigureLogging(logging => logging.AddConsole());
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

