using MemberManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemberManagement.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MemberManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}