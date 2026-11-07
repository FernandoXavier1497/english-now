using System.Globalization;
using EnglishNow.Repositories;
using EnglishNow.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Exige o token anti-CSRF em todo POST (os formulários já enviam o token automaticamente)
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {
        Options.LoginPath = "/login";
        Options.AccessDeniedPath = "/acesso-negado";
        Options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Configuration.AddEnvironmentVariables();


builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IProfessorService, ProfessorService>();

builder.Services.AddScoped<IAlunoService, AlunoService>();

builder.Services.AddScoped<ITurmaService, TurmaService>();

builder.Services.AddScoped<IBoletimService, BoletimService>();


var connectionString = builder.Configuration.GetConnectionString("EnglishNowConnectionString")
    ?? throw new InvalidOperationException(
        "Connection string 'EnglishNowConnectionString' não encontrada. Veja o README (user-secrets ou appsettings).");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString));

builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString));

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString));

builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString));

builder.Services.AddScoped<IBoletimRepository, BoletimRepository>(c => new BoletimRepository(connectionString));

var app = builder.Build();

//Usando tela de erro customizada mesmo em ambiente de desenvolvimento
app.UseExceptionHandler("/Erro/Index");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Cultura pt-BR: as notas são digitadas com vírgula (7,5) e precisam ser lidas assim no formulário
var culturaPtBr = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culturaPtBr),
    SupportedCultures = new List<CultureInfo> { culturaPtBr },
    SupportedUICultures = new List<CultureInfo> { culturaPtBr }
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
