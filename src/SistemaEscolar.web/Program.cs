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
// URLs em minúsculas
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

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
var connectionStringSqlServer = builder.Configuration.GetConnectionString("SistemaEscolarConnectionStringSqlServer");

// Repositórios: Se a connection string do SQL Server estiver configurada, usa SQL Server, senão usa MySQL
if (!string.IsNullOrEmpty(connectionStringSqlServer))
{
    // Repositórios SQL Server
    builder.Services.AddScoped<IUsuarioRepository, UsuarioSqlServerRepository>(_ => new UsuarioSqlServerRepository(connectionStringSqlServer));
    builder.Services.AddScoped<IProfessorRepository, ProfessorSqlServerRepository>(_ => new ProfessorSqlServerRepository(connectionStringSqlServer));
    builder.Services.AddScoped<IAlunoRepository, AlunoSqlServerRepository>(_ => new AlunoSqlServerRepository(connectionStringSqlServer));
    builder.Services.AddScoped<ITurmaRepository, TurmaSqlServerRepository>(_ => new TurmaSqlServerRepository(connectionStringSqlServer));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimSqlServerRepository>(_ => new AlunoTurmaBoletimSqlServerRepository(connectionStringSqlServer));
}
else
{
    // Repositórios MySQL (padrão)
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(_ => new UsuarioRepository(connectionString!));
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(_ => new ProfessorRepository(connectionString!));
    builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(_ => new AlunoRepository(connectionString!));
    builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(_ => new TurmaRepository(connectionString!));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(_ => new AlunoTurmaBoletimRepository(connectionString!));
}

var app = builder.Build();

app.UseExceptionHandler("/Erro/Index");


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Localiza��o pt-BR
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