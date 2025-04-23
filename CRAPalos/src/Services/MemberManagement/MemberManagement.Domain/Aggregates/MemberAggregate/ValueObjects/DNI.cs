using Common.Domain.Attributes;
using Common.Domain.ValueObjects;

namespace MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects
{
    [StringValueObjectMetadata(Description.MAX_LENGTH, Description.REQUIRED)]
    public sealed class DNI : StringValueObject<DNI>
    {
        public DNI(string value) : base(value, Description.REQUIRED, Description.MAX_LENGTH)
        {
        }

        public static DNI Create(string value) => new(value);

        public static implicit operator DNI(string value) => Create(value);

        public static class Description
        {
            public const int MAX_LENGTH = 50;
            public const bool REQUIRED = true;
        }
    }
}
