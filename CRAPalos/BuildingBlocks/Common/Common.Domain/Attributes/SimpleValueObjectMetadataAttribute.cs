namespace Common.Domain.Attributes;

public abstract class SimpleValueObjectMetadataAttribute : Attribute
{
    public Type UnderlyingType { get; }
    public bool Required { get; }

    protected SimpleValueObjectMetadataAttribute(Type underlyingType, bool required = false)
    {
        UnderlyingType = underlyingType;
        Required = required;
    }
}
