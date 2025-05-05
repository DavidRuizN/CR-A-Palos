using Common.Domain.Entities;
using MemberManagement.Domain.Aggregates.MemberAggregate.ValueObjects;

namespace MemberManagement.Domain.Aggregates.MemberAggregate;

public class Member : Entity<Guid>
{
    public Name Name { get; private set; }
    public Surnames Surnames { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DNI DNI { get; private set; }
    public MembershipType Type { get; private set; }
    public byte[] ProfilePicture { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public MembershipPayment MembershipPayment { get; private set; }

    private Member()
    {
        // EF Core requires a parameterless constructor for materialization
    }

    private Member(Name name, Surnames surnames, Address address, PhoneNumber phoneNumber, Email email, DateTime birthDate,
        DNI dni, MembershipType type)
    {
        Name = name;
        Surnames = surnames;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        BirthDate = birthDate;
        DNI = dni;
        Type = type;
        CreatedAt = DateTime.UtcNow;
    }

    public static Member Create(Name name, Surnames surnames, Address address, PhoneNumber phoneNumber, Email email, DateTime birthDate,
        DNI dni, MembershipType type)
    {
        return new Member(name, surnames, address, phoneNumber, email, birthDate, dni, type);
    }

    public void UpdateProfilePicture(byte[] picture)
    {
        ProfilePicture = picture;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(Name name, Surnames surnames, Address address, PhoneNumber phoneNumber, Email email, DateTime birthDate,
        DNI dni, MembershipType type)
    {
        Name = name;
        Surnames = surnames;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        BirthDate = birthDate;
        DNI = dni;
        Type = type;
        UpdatedAt = DateTime.UtcNow;
    }
}