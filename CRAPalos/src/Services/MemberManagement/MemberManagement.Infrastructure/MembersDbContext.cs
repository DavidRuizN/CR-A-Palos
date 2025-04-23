using Common.Infrastructure.Database;
using MediatR;
using MemberManagement.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure
{
    public class MembersDbContext : BaseContext
    {
        public MembersDbContext(DbContextOptions<MembersDbContext> options) : base(options)
        {
        }

        public MembersDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
