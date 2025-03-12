using EnglishNow.Repositories;
using EnglishNow.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {
        Options.LoginPath = "/login";
        Options.AccessDeniedPath = "/login";
        Options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var connectionString = builder.Configuration.GetConnectionString("EnglishNowConnectionString");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString!));

WebApplication app = builder.Build();

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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
