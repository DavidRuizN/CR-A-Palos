using MemberManagement.Domain.Aggregates.MemberAggregate;
using MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MemberManagement.Infrastructure.EntityConfigurations;

public class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .HasConversion(new ValueConverter<Name, string>(n => n, Value => Name.Create(Value)))
            .IsRequired(Name.Description.REQUIRED)
            .HasMaxLength(Name.Description.MAX_LENGTH);

        builder.Property(m => m.Surnames)
            .HasConversion(new ValueConverter<Surnames, string>(n => n, Value => Surnames.Create(Value)))
            .IsRequired(Surnames.Description.REQUIRED)
            .HasMaxLength(Surnames.Description.MAX_LENGTH);

        AddressEntityConfiguration.Configure(builder, s => s.Address, nameof(Member.Address));

        builder.Property(m => m.Email)
            .HasConversion(new ValueConverter<Email, string>(n => n, Value => Email.Create(Value)))
            .IsRequired(Email.Description.REQUIRED)
            .HasMaxLength(Email.Description.MAX_LENGTH);

        builder.Property(m => m.PhoneNumber)
            .HasConversion(new ValueConverter<PhoneNumber, string>(n => n, Value => PhoneNumber.Create(Value)))
            .IsRequired(PhoneNumber.Description.REQUIRED)
            .HasMaxLength(PhoneNumber.Description.MAX_LENGTH);

        builder.Property(m => m.BirthDate).IsRequired();

        builder.Property(m => m.DNI)
            .HasConversion(new ValueConverter<DNI, string>(n => n, Value => DNI.Create(Value)))
            .IsRequired(DNI.Description.REQUIRED)
            .HasMaxLength(DNI.Description.MAX_LENGTH);

        builder.Property(m => m.Type).IsRequired();

        builder.Property(m => m.ProfilePicture);

        builder.Property(m => m.CreatedAt).IsRequired();
        builder.Property(m => m.UpdatedAt);

        builder.HasOne(m => m.MembershipPayment)
            .WithOne()
            .HasForeignKey<MembershipPayment>(mp => mp.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
