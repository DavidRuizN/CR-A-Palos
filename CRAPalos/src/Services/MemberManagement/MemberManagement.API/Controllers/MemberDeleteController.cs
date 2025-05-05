using Common.API.Models;
using MediatR;
using MemberManagement.API.Application.Commands;
using MemberManagement.Domain.Aggregates.MemberAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MemberManagement.API.Controllers;

[Route("api/v1/members")]
[ApiController]
public class MemberDeleteController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberDeleteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Member>> Invoke([FromRoute] Guid id)
    {
        var command = new MemberCommand
        {
            Operation = OperationType.Delete,
            Id = id
        };

        var result = await _mediator.Send(command);

        if (!result) return BadRequest();

        return Ok();
    }
}