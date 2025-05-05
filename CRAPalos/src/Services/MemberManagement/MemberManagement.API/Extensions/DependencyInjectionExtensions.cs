using Common.Domain.Interfaces;
using Common.Infrastructure.Database;
using FluentValidation;
using MemberManagement.API.Application.Queries;
using MemberManagement.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MemberManagement.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static void InyectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar Patrón de Repositorio
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MemberManagementDbContext>());
        services.AddScoped<BaseContext>(sp => sp.GetRequiredService<MemberManagementDbContext>());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Registrar FluentValidation y MediatR
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Queries
        services.AddScoped<IMemberQueries, MemberQueries>();

        // Services
    }
}