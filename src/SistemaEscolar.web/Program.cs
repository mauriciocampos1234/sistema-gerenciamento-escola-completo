using SistemaEscolar.Repositories;
using SistemaEscolar.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using SistmaEscolar.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IBoletimService, BoletimService>();

var connectionString = builder.Configuration.GetConnectionString("SistemaEscolarConnectionString");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(_ => new UsuarioRepository(connectionString!));
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(_ => new ProfessorRepository(connectionString!));
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(_ => new AlunoRepository(connectionString!));
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(_ => new TurmaRepository(connectionString!));
builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(_ => new AlunoTurmaBoletimRepository(connectionString!));

var app = builder.Build();

app.UseExceptionHandler("/Erro/Index");


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Localização pt-BR
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Endpoints para Razor Pages e Controllers
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
