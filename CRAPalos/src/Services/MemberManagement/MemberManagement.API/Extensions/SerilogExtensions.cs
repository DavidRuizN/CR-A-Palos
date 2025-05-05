using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MemberManagement.API.Extensions;

public static class SerilogExtensions
{
    public static void ConfigureSerilog(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext() // Añadir contexto de logs
            .Enrich.WithMachineName() // Añadir nombre de la máquina
            .Enrich.WithThreadId() // Añadir ID del hilo
            .ReadFrom.Configuration(configuration) // Leer configuración desde appsettings.json
            .CreateLogger();

        Log.Information("Iniciando MemberManagement.API");

        // Usar Serilog como el proveedor de logging
        hostBuilder.UseSerilog();
    }
}