using MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;

namespace MemberManagement.Domain.Aggregates.MemberAggregate;

public class Address
{
    public Street Street { get; private set; }
    public Number Number { get; private set; }
    public AdditionalInformation AdditionalInformation { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public Town Town { get; private set; }
    public CountryName CountryName { get; private set; }

    private Address(Street street, Number number, AdditionalInformation additionalInformation, ZipCode zipCode, Town town, CountryName countryName)
    {
        Street = street;
        Number = number;
        AdditionalInformation = additionalInformation;
        ZipCode = zipCode;
        Town = town;
        CountryName = countryName;
    }

    public static Address Create(Street street, Number number, AdditionalInformation additionalInformation, ZipCode zipCode, Town town, CountryName countryName)
    {
        return new Address(street, number, additionalInformation, zipCode, town, countryName);
    }
}
