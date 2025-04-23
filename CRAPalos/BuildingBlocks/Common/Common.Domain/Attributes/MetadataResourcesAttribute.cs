namespace Common.Domain.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public class MetadataResourcesAttribute : Attribute
{
    public MetadataResourcesAttribute(Type resourcesType)
    {
        ResourceType = resourcesType;
    }

    public Type ResourceType { get; }
}
