using Common.API.Models;
using Common.Domain.Exceptions;
using Common.Domain.Interfaces;
using MediatR;
using MemberManagement.Domain.Aggregates.MemberAggregate;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MemberManagement.API.Application.Commands.Handlers;

public class MemberCommandHandler : IRequestHandler<MemberCommand, bool>
{
    private readonly ILogger<MemberCommandHandler> _logger;
    private readonly IRepository<Member> _repository;

    public MemberCommandHandler(ILogger<MemberCommandHandler> logger, IRepository<Member> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<bool> Handle(MemberCommand request, CancellationToken cancellationToken)
    {
        Member member;

        if (request.Operation == OperationType.Create)
        {
            _logger.LogInformation("Creating member with ID: {Id}", request.Id);
            var address = Address.Create(request.Address.Street, request.Address.Number, request.Address.AdditionalInformation,
                request.Address.ZipCode, request.Address.Town, request.Address.CountryName);

            member = Member.Create(request.Name, request.Surnames, address, request.PhoneNumber, request.Email,
                request.BirthDate, request.DNI, request.Type);

            _repository.Add(member);
        }
        else
        {
            member = await _repository.GetByIdAsync(request.Id);

            if (member == null)
            {
                _logger.LogInformation("Member with ID: {Id} not found", request.Id);
                return request.Operation == OperationType.Update
                    ? throw new DomainException(string.Format("Member with ID: {Id} not found", request.Id))
                    : true;
            }
            if (request.Operation == OperationType.Update)
            {
                _logger.LogInformation("Updating member with ID: {Id}", request.Id);

                var address = Address.Create(request.Address.Street, request.Address.Number, request.Address.AdditionalInformation,
                    request.Address.ZipCode, request.Address.Town, request.Address.CountryName);

                member.Update(request.Name, request.Surnames, address, request.PhoneNumber, request.Email,
                    request.BirthDate, request.DNI, request.Type);

                _repository.Update(member);
            }
            else
            {
                _logger.LogInformation("Deleting member with ID: {Id}", request.Id);

                _repository.Delete(member);
            }
        }
        if (!await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
        {
            _logger.LogError("Failed to save changes for member with ID: {Id}", request.Id);
            throw new DomainException(string.Format("Failed to save changes for member with ID: {Id}", request.Id));
        }

        _logger.LogInformation("Member with ID: {Id} successfully processed", request.Id);

        return true;
    }
}