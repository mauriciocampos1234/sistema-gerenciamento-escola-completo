using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaEscolar.Repositories;
using SistemaEscolar.Services;

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

// Usando pagina de erro personalizada mesmo em ambiente de desenvolvimento
app.UseExceptionHandler("/Erro/Index"); //Agora voltamos ele para o escopo do IF

// Configure o pipeline de solicitação HTTP.
//Significado do if: Se não for ambiente de desenvolvimento, vamos usa uma tela de erro customizada (Ambiente de produção)
if (!app.Environment.IsDevelopment())
{
    // Agora ele vai usar a tela de erro  dele mesmo, do próprio ASP.NET, quando estiver em desenvolvimento
    //app.UseExceptionHandler("/Erro/Index");
    // O valor padrão do HSTS é 30 dias. Você pode querer alterar isso para cenários de produção, consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
