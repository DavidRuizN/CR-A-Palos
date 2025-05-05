using MemberManagement.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.API.Application.Queries;

public interface IMemberQueries
{
    Task<MemberDTO> GetMemberByIdAsync(Guid memberId);

    Task<IEnumerable<MemberDTO>> GetAllMembersAsync();
}