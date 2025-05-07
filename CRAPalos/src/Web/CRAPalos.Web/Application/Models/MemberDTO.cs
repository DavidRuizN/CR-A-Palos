using CRAPalos.Web.Application.Models.Enums;

namespace CRAPalos.Web.Application.Models;

public class MemberDTO : AddressDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surnames { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string DNI { get; set; }
    public MembershipType Type { get; set; }
    public byte[] ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}