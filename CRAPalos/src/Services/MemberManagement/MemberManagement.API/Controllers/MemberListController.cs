using MemberManagement.API.Application.Models;
using MemberManagement.API.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MemberManagement.API.Controllers;

[Route("api/v1/members")]
[ApiController]
public class MemberListController : ControllerBase
{
    private readonly IMemberQueries _queries;

    public MemberListController(IMemberQueries queries)
    {
        _queries = queries;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MemberDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> Invoke()
    {
        var member = await _queries.GetAllMembersAsync();

        if (member == null || !member.Any())
        {
            return NotFound();
        }

        return Ok(member);
    }
}