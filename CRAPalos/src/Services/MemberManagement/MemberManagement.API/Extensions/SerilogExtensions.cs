using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MemberManagement.API.Extensions;

public static class SerilogExtensions
{
    public static void ConfigureSerilog(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext() // A�adir contexto de logs
            .Enrich.WithMachineName() // A�adir nombre de la m�quina
            .Enrich.WithThreadId() // A�adir ID del hilo
            .ReadFrom.Configuration(configuration) // Leer configuraci�n desde appsettings.json
            .CreateLogger();

        Log.Information("Iniciando MemberManagement.API");

        // Usar Serilog como el proveedor de logging
        hostBuilder.UseSerilog();
    }
}