using CRAPalos.App.Application.Services;
using CRAPalos.App.Components;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Registrar HttpClient
builder.Services.AddHttpClient();

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped(x => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("Urls").GetValue<string>("Members"))
});

// Registrar servicios de Radzen
builder.Services.AddRadzenComponents();

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();