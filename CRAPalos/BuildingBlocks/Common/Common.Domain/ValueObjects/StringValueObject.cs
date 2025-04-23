using Common.Domain.Exceptions;
using Common.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Common.Domain.ValueObjects;

public abstract class StringValueObject<T> : IEquatable<T>, IComparable<T>, ISimpleValueObject where T : StringValueObject<T>
{
    private readonly string value;

    public StringValueObject(string value, bool required = false, int? maxLength = default, string? regexPattern = default)
    {
        if (required && string.IsNullOrEmpty(value))
        {
            throw new DomainException(string.Format(CommonResources.RequiredValueObject, GetType().Name));
        }

        if (maxLength.HasValue && !string.IsNullOrEmpty(value) && value.Length > maxLength)
        {
            throw new DomainException(string.Format(CommonResources.LengthValueObject, GetType().Name, maxLength, value));
        }

        if (!string.IsNullOrEmpty(regexPattern) && !string.IsNullOrEmpty(value))
        {
            var regexStrict = new Regex(regexPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

            if (!regexStrict.IsMatch(value))
            {
                throw new DomainException(string.Format(CommonResources.RegexValueObject, GetType().Name, value));
            }
        }

        this.value = value;
    }

    protected StringValueObject(string value, int? maxLength = default) : this(value, false, maxLength)
    {
    }

    public int CompareTo(T? other)
    {
        if (other is null) return 1;
        return value.CompareTo(other.value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as T);
    }

    public bool Equals(T? other)
    {
        if (ReferenceEquals(other, null)) return false;

        if (ReferenceEquals(this, other)) return true;

        return Equals(value, other.value);
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public static bool operator ==(StringValueObject<T> x, StringValueObject<T> y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (((object)x == null) || ((object)y == null))
        {
            return false;
        }

        return x.Equals(y);
    }

    public static bool operator !=(StringValueObject<T> x, StringValueObject<T> y)
    {
        return !(x == y);
    }

    public static implicit operator string?(StringValueObject<T> stringValue)
    {
        return stringValue?.value;
    }

    public override string ToString()
    {
        return value;
    }

    object ISimpleValueObject.GetValue()
    {
        return value;
    }
}
