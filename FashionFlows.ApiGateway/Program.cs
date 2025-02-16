using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);


var ocelotConfig = new ConfigurationBuilder().AddJsonFile("ocelot.json", false, true).Build();
builder.Services.AddOcelot(ocelotConfig)
        .AddConsul();

var app = builder.Build();

app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
