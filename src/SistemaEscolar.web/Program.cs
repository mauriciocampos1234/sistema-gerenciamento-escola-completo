var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
