using CRAPalos.App.Application.Models;
using CRAPalos.App.Application.Models.Enums;

namespace CRAPalos.App.Application.Commands;

public class MemberCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surnames { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public AddressDTO Address { get; set; }
    public DateTime BirthDate { get; set; }
    public string DNI { get; set; }
    public MembershipType Type { get; set; }
    public byte[] ProfilePicture { get; set; }
}