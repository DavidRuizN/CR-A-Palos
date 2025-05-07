using CRAPalos.Web.Application.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CRAPalos.Web.Pages;

public class MembersModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MembersModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<MemberDTO> Members { get; set; } = new();

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("MemberManagementAPI");
        var response = await client.GetAsync("members");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Members = JsonSerializer.Deserialize<List<MemberDTO>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];
        }
    }
}