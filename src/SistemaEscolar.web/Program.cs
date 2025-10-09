using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaEscolar.Repositories;
using SistemaEscolar.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Caminho da Página de login 
        options.AccessDeniedPath = "/Login"; // Página de acesso negado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Tempo de expiração do cookie, após 10 minutos de inatividade
        options.SlidingExpiration = true; // Renova o cookie se o usuário estiver ativo
    });

builder.Configuration.AddEnvironmentVariables(); //Apontando e Adicionando suporte para variáveis de ambiente do Windows

//Mapeando a injeção de dependência do serviço de usuário (IUsuarioService)
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//Mapeando a injeção de dependência do repositório de usuário (IUsuarioRepository)
var connectionString = builder.Configuration.GetConnectionString("SistemaEscolarConnectionString"); //string.Empty; Inicializando a variável connectionString (Mas vamos deixar Empty(Vazia) por enquanto)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString!));


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
