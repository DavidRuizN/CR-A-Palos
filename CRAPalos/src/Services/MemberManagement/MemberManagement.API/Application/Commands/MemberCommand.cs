using Common.API.Models;
using MediatR;
using MemberManagement.API.Application.Models;
using MemberManagement.Domain.Aggregates.MemberAggregate;
using System;

namespace MemberManagement.API.Application.Commands;

public class MemberCommand : IRequest<bool>
{
    public OperationType Operation { get; set; }
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