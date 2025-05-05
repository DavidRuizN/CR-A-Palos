using MemberManagement.Domain.Aggregates.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemberManagement.Infrastructure.EntityConfigurations;

public class MembershipPaymentEntityConfiguration : IEntityTypeConfiguration<MembershipPayment>
{
    public void Configure(EntityTypeBuilder<MembershipPayment> builder)
    {
        builder.ToTable("MembershipPayments");
        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.MemberId)
            .IsRequired();

        builder.Property(mp => mp.Amount)
            .IsRequired();

        builder.Property(mp => mp.PaidAt);

        builder.Property(mp => mp.Method)
            .IsRequired();

        builder.Property(mp => mp.Status)
            .IsRequired();

        builder.Property(mp => mp.CreatedAt)
            .IsRequired();
    }
}
