using CRAPalos.App.Application.Models;

namespace CRAPalos.App.Application.Services;

public interface IMemberService
{
    Task<IEnumerable<MemberDTO>> GetMembersAsync();

    Task<MemberDTO> GetMemberByIdAsync(Guid id);

    Task AddMemberAsync(MemberDTO member);

    Task UpdateMemberAsync(MemberDTO member);

    Task DeleteMemberAsync(Guid id);
}