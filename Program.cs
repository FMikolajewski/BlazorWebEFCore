using BlazorWebAppEFCore.Components;
using Microsoft.EntityFrameworkCore;
using BlazorWebAppEFCore.Data;
using BlazorWebAppEFCore.Grid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register factory and configure the options
// <snippet1>
builder.Services.AddDbContextFactory<ContactContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(ContactContext.ContactsDb)}.db"));
// </snippet1>

// Pager
builder.Services.AddScoped<IPageHelper, PageHelper>();

// Filters
builder.Services.AddScoped<IContactFilters, GridControls>();

// Query adapter (applies filter to contact request)
builder.Services.AddScoped<GridQueryAdapter>();

// Service to communicate success on edit between pages
builder.Services.AddScoped<EditSuccess>();

var app = builder.Build();

// Esta sección configura y inicializa la base de datos. Normalmente NO se siembra
// manejado de esta manera en producción. En este caso se utiliza el siguiente enfoque
// aplicación de muestra para simplificar la muestra. La aplicación se puede clonar. El
// la cadena de conexión está configurada. La aplicación se puede ejecutar.
await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<ContactContext>>();
await DatabaseUtility.EnsureDbCreatedAndSeedWithCountOfAsync(options, 500);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
