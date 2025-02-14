using FashionFlows.BuildingBlock.Application.Configuration;
using FashionFlows.BuildingBlock.Application.Middleware;
using FashionFlows.BuildingBlock.Infrastructure.GenericInstaller;
using FashionFlows.Payment.Infrastructure.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureSerilog();
builder.Services.AddControllers();

builder.Services.AddScoped<GlobalException>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FashionFlows", Version = "v1" });
    c.EnableAnnotations();
});


builder.Services.InstallServicesInAssembly<IPaymentAssembly>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureGlobalExceptionHandler();

app.Run();
