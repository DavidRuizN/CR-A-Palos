namespace Common.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class StringValueObjectMetadataAttribute : SimpleValueObjectMetadataAttribute
{
    public int? MaxLength { get; }
    public string Pattern { get; }

    public StringValueObjectMetadataAttribute(int maxLength = default, bool required = false) : base(typeof(string), required)
    {
        MaxLength = maxLength;
    }

    public StringValueObjectMetadataAttribute(int maxLength = default, string pattern = null, bool required = false) : base(typeof(string), required)
    {
        MaxLength = maxLength;
        Pattern = pattern;
    }

    public StringValueObjectMetadataAttribute(bool required = false) : base(typeof(string), required)
    {
    }
}
