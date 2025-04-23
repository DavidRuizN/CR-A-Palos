using MemberManagement.Domain.Aggregates.MemberAggregate;
using MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace MemberManagement.Infrastructure.EntityConfigurations
{
    public static class AddressEntityConfiguration
    {
        public static void Configure<T>(EntityTypeBuilder<T> builder,
            [NotNull] Expression<Func<T, Address>> nav, string tablePrefix) where T : class
        {
            builder.OwnsOne(nav).Property(a => a.Street)
                .HasColumnName(tablePrefix + nameof(Address.Street))
                .HasConversion(new ValueConverter<Street, string>(n => n, Value => Street.Create(Value)))
                .HasMaxLength(Street.Description.MAX_LENGTH);

            builder.OwnsOne(nav).Property(a => a.Number)
                .HasColumnName(tablePrefix + nameof(Address.Number))
                .HasConversion(new ValueConverter<Number, string>(n => n, Value => Number.Create(Value)))
                .HasMaxLength(Number.Description.MAX_LENGTH);

            builder.OwnsOne(nav).Property(a => a.AdditionalInformation)
                .HasColumnName(tablePrefix + nameof(Address.AdditionalInformation))
                .HasConversion(new ValueConverter<AdditionalInformation, string>(n => n, Value => AdditionalInformation.Create(Value)))
                .HasMaxLength(AdditionalInformation.Description.MAX_LENGTH);

            builder.OwnsOne(nav).Property(a => a.ZipCode)
                .HasColumnName(tablePrefix + nameof(Address.ZipCode))
                .HasConversion(new ValueConverter<ZipCode, string>(n => n, Value => ZipCode.Create(Value)))
                .HasMaxLength(ZipCode.Description.MAX_LENGTH);

            builder.OwnsOne(nav).Property(a => a.Town)
                .HasColumnName(tablePrefix + nameof(Address.Town))
                .HasConversion(new ValueConverter<Town, string>(n => n, Value => Town.Create(Value)))
                .HasMaxLength(Town.Description.MAX_LENGTH);

            builder.OwnsOne(nav).Property(a => a.CountryName)
                .HasColumnName(tablePrefix + nameof(Address.CountryName))
                .HasConversion(new ValueConverter<CountryName, string>(n => n, Value => CountryName.Create(Value)))
                .HasMaxLength(CountryName.Description.MAX_LENGTH);
        }
    }
}
