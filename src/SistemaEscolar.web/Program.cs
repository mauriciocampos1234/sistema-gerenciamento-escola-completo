using SistemaEscolar.Repositories;
using SistemaEscolar.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
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

// REGISTRAR SERVICES (faltando)
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IBoletimService, BoletimService>();

builder.Configuration.AddEnvironmentVariables();

<<<<<<< Updated upstream
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString!));
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString!));
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString!));
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString!));
builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(c => new AlunoTurmaBoletimRepository(connectionString!));

=======
var provider = builder.Configuration["DatabaseSettings:Provider"]?.ToLowerInvariant();
if (string.IsNullOrWhiteSpace(provider))
    throw new InvalidOperationException("Config 'DatabaseSettings:Provider' ausente.");

var connectionString = provider switch
{
    "mysql" => builder.Configuration.GetConnectionString("MySqlConnection")
              ?? throw new InvalidOperationException("Connection string 'MySqlConnection' n√£o encontrada."),
    "sqlserver" => builder.Configuration.GetConnectionString("SqlServerConnection")
              ?? throw new InvalidOperationException("Connection string 'SqlServerConnection' n√£o encontrada."),
    _ => throw new InvalidOperationException($"Provider '{provider}' n√£o √© suportado. Use 'mysql' ou 'sqlserver'.")
};

// Registrar reposit√≥rios baseado no provider
if (provider == "mysql")
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString));
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString));
    builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString));
    builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(c => new AlunoTurmaBoletimRepository(connectionString));
}
else if (provider == "sqlserver")
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorySqlServer>(c => new UsuarioRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepositorySqlServer>(c => new ProfessorRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IAlunoRepository, AlunoRepositorySqlServer>(c => new AlunoRepositorySqlServer(connectionString));
    builder.Services.AddScoped<ITurmaRepository, TurmaRepositorySqlServer>(c => new TurmaRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepositorySqlServer>(c => new AlunoTurmaBoletimRepositorySqlServer(connectionString));
}
>>>>>>> Stashed changes

var app = builder.Build();


<<<<<<< Updated upstream
app.UseExceptionHandler("/Erro/Index");

=======
>>>>>>> Stashed changes
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

<<<<<<< Updated upstream
// ConfiguraÁ„o de localizaÁ„o para pt-BR
=======
>>>>>>> Stashed changes
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