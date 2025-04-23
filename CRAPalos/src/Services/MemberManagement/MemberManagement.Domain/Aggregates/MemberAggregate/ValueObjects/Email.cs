using Common.Domain.Attributes;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;

[StringValueObjectMetadata(Description.MAX_LENGTH, Description.REQUIRED)]
public sealed class Email : StringValueObject<Email>
{
    public Email(string value) : base(value, Description.REQUIRED, Description.MAX_LENGTH)
    {
        if (!IsValid(value))
        {
            throw new DomainException($"Email {value} is not valid");
        }
    }

    public static Email Create(string value) => new(value);

    public static Email CreateIfValid(string value)
    {
        return IsValid(value) ? new Email(value) : null;
    }

    public static implicit operator Email(string value) => Create(value);

    public static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value)) return !Description.REQUIRED;

        var patternStrict = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; // Patrón para correos electrónicos
        var regexStrict = new Regex(patternStrict);

        return regexStrict.IsMatch(value);
    }

    public static class Description
    {
        public const int MAX_LENGTH = 50;
        public const bool REQUIRED = true;
    }
}