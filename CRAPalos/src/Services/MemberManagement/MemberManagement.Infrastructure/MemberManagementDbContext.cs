using Common.Infrastructure.Database;
using MemberManagement.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure;

public class MemberManagementDbContext : BaseContext
{
    public MemberManagementDbContext(DbContextOptions<MemberManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
