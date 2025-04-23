using MemberManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Common.Infrastructure.Database;

public class DbContextFactory : IDesignTimeDbContextFactory<MembersDbContext>
{
    private const string ConnectionString = "Server=192.168.0.53;Database=Services.Members;User Id=sa;Password=sqlserver;TrustServerCertificate=True;";

    public MembersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MembersDbContext>();
        optionsBuilder.UseSqlServer(ConnectionString, options => options.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));

        return new MembersDbContext(optionsBuilder.Options);
    }
}
