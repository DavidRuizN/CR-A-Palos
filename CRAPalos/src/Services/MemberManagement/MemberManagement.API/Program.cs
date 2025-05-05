using MemberManagement.API.Extensions;
using MemberManagement.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog(builder.Configuration); // Configurar Serilog

builder.Services.ConfigureDatabase(builder.Configuration); // Configurar BBDD

// Registrar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InyectDependencies(builder.Configuration); // Inyección de dependencias

var app = builder.Build();
Log.Information("MemberManagement.API iniciado correctamente");

// Ejecutar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MemberManagementDbContext>();
    dbContext.Database.Migrate();
}

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Log.Information("MemberManagement.API está escuchando en la URL {Url}", app.Configuration["ASPNETCORE_URLS"]);

app.Run();