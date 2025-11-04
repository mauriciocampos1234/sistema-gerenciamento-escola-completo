using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using SistemaEscolar.Repositories;
using SistemaEscolar.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var connectionString = builder.Configuration.GetConnectionString("SistemaEscolarConnectionString");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString!));
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString!));
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString!));
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString!));
builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(c => new AlunoTurmaBoletimRepository(connectionString!));


var app = builder.Build();


app.UseExceptionHandler("/Erro/Index");


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
