using MemberManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Common.Infrastructure.Database;

public class DbContextFactory : IDesignTimeDbContextFactory<MemberManagementDbContext>
{
    private const string ConnectionString = "Server=192.168.0.53;Database=Services.Members;User Id=sa;Password=sqlserver;TrustServerCertificate=True;";

    public MemberManagementDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MemberManagementDbContext>();
        optionsBuilder.UseSqlServer(ConnectionString, options => options.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));

        return new MemberManagementDbContext(optionsBuilder.Options);
    }
}
