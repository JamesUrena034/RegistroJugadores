using Microsoft.EntityFrameworkCore;
using RegistroJugadores.Components;
using RegistroJugadores.DAL;
using RegistroJugadores.Services;
using RegistroJugadoresServer.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var conStr = builder.Configuration.GetConnectionString("SqliteConStr");
builder.Services.AddDbContextFactory<Contexto>(options => options.UseSqlite(conStr));


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://gestionhuacalesapi.azurewebsites.net/")
});

builder.Services.AddScoped<JugadoresService>();
builder.Services.AddScoped<PartidasService>();
builder.Services.AddScoped<MovimientosService>();
builder.Services.AddScoped<IMovimientosApiService, MovimientosApiService>();
builder.Services.AddScoped<IPartidasApiService, PartidasApiService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
