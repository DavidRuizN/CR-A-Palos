namespace Common.Infrastructure.Database;

public class DbContextFactory : IDesignTimeDbContextFactory<TicketsDbContext>
{
    private const string ConnectionString = "Server=192.168.0.53;Database=CRM.Services.Tickets;User Id=sa;Password=sqlserver;TrustServerCertificate=True;";

    public TicketsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TicketsDbContext>();
        optionsBuilder.UseSqlServer(ConnectionString, options => options.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));

        return new TicketsDbContext(optionsBuilder.Options);
    }
}
