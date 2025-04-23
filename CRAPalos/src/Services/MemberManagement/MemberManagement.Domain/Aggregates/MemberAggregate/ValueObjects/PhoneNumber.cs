using Common.Domain.Attributes;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;

[StringValueObjectMetadata(Description.MAX_LENGTH, Description.REQUIRED)]
public sealed class PhoneNumber : StringValueObject<PhoneNumber>
{
    public PhoneNumber(string value) : base(value, Description.REQUIRED, Description.MAX_LENGTH)
    {
        if (!IsValid(value))
        {
            throw new DomainException($"Phone number {value} is not valid");
        }
    }

    public static PhoneNumber Create(string value) => new(value);

    public static PhoneNumber CreateIfValid(string value)
    {
        value = value?.Replace(" ", "").Replace("-", "");
        return IsValid(value) ? new PhoneNumber(value) : null;
    }

    public static implicit operator PhoneNumber(string value) => Create(value);

    public static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value)) return !Description.REQUIRED;
        var patternStrict = @"^(\+34)?[6789]\d{8}$"; // Patrón para números españoles, que comienzan opcionalmente en 34 y contienen 8 dígitos subsiguientes.
        var regexStrict = new Regex(patternStrict);
        return regexStrict.IsMatch(value);
    }

    public static class Description
    {
        public const int MAX_LENGTH = 13;
        public const bool REQUIRED = true;
    }
}
