using PersonalScheduling.BuildingBlock.Infrastructure.GenericInstaller;
using PersonalScheduling.Services.User.Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InstallServicesInAssembly<IUserAssembly>(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
