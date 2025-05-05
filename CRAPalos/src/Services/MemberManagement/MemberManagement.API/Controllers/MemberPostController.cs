using Common.API.Models;
using MediatR;
using MemberManagement.API.Application.Commands;
using MemberManagement.Domain.Aggregates.MemberAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MemberManagement.API.Controllers;

[Route("api/v1/members")]
[ApiController]
public class MemberPostController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberPostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Member>> Invoke([FromBody] MemberCommand command)
    {
        command.Operation = OperationType.Create;

        var result = await _mediator.Send(command);

        if (!result) return BadRequest();

        return Created("", "");
    }
}