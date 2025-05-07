using CRAPalos.App.Application.Commands;
using CRAPalos.App.Application.Models;

namespace CRAPalos.App.Application.Services;

public class MemberService : IMemberService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "members/";

    public MemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddMemberAsync(MemberDTO member)
    {
        var memberCommand = new MemberCommand()
        {
            Name = member.Name,
            Surnames = member.Surnames,
            PhoneNumber = member.PhoneNumber,
            Email = member.Email,
            Address = new AddressDTO
            {
                Street = member.Street,
                Number = member.Number,
                Town = member.Town,
                CountryName = member.CountryName,
                ZipCode = member.ZipCode,
                AdditionalInformation = member.AdditionalInformation
            },
            BirthDate = member.BirthDate,
            DNI = member.DNI,
            Type = member.Type,
            ProfilePicture = member.ProfilePicture
        };

        await _httpClient.PostAsJsonAsync<MemberCommand>(_baseUrl, memberCommand);
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
    }

    public async Task<MemberDTO> GetMemberByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<MemberDTO>($"{_baseUrl}/{id}");
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<MemberDTO>>(_baseUrl);
    }

    public async Task UpdateMemberAsync(MemberDTO member)
    {
        var memberCommand = new MemberCommand()
        {
            Name = member.Name,
            Surnames = member.Surnames,
            PhoneNumber = member.PhoneNumber,
            Email = member.Email,
            Address = new AddressDTO
            {
                Street = member.Street,
                Number = member.Number,
                Town = member.Town,
                CountryName = member.CountryName,
                ZipCode = member.ZipCode,
                AdditionalInformation = member.AdditionalInformation
            },
            BirthDate = member.BirthDate,
            DNI = member.DNI,
            Type = member.Type,
            ProfilePicture = member.ProfilePicture
        };

        await _httpClient.PutAsJsonAsync<MemberCommand>($"{_baseUrl}/{memberCommand.Id}", memberCommand);
    }
}