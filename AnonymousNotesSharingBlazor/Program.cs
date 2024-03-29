using AnonymousNotesSharingBlazor.Components;
using AnonymousNotesSharingBlazor.Data;
using AnonymousNotesSharingBlazor.Hubs;
using AnonymousNotesSharingBlazor.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using ILogger = AnonymousNotesSharingBlazor.Services.ILogger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});
services.AddRazorComponents()
    .AddInteractiveServerComponents();
services.AddBlazorBootstrap();
services.AddDbContextFactory<NotesContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NotesDB")));
services.AddScoped<INoteDataService, NotesDataService>();
services.AddScoped<ILogger, DebugLogger>();
services.AddScoped<INoteHubProxy, NoteHubProxy>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<NoteHub>("/noteshub");

app.Run();
