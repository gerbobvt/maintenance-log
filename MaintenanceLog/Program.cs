using MaintenanceLog;
using MaintenanceLog.Components;
using MaintenanceLog.DataAccess;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<MaintenanceLogContext>(opt =>
    opt.UseSqlite("Data Source=MaintenanceLog.db"));

var app = builder.Build();

await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<MaintenanceLogContext>>();
await DbUtils.EnsureDbCreated(options);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
