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
public class MemberPutController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberPutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Member>> Invoke([FromBody] MemberCommand command, [FromRoute] Guid id)
    {
        command.Operation = OperationType.Update;
        command.Id = id;

        var result = await _mediator.Send(command);

        if (!result) return BadRequest();

        return Ok();
    }
}