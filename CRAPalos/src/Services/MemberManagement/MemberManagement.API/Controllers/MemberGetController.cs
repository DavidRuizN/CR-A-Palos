using MemberManagement.API.Application.Models;
using MemberManagement.API.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MemberManagement.API.Controllers;

[Route("api/v1/members")]
[ApiController]
public class MemberGetController : ControllerBase
{
    private readonly IMemberQueries _queries;

    public MemberGetController(IMemberQueries queries)
    {
        _queries = queries;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MemberDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<MemberDTO>> Invoke([FromRoute] Guid id)
    {
        var member = await _queries.GetMemberByIdAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }
}