using Blazored.LocalStorage;
using DataBase;
using MovieSearcher.Client.Pages;
using MovieSearcher.Components;
using MovieSearcher.Services;
using MovieSearcher.SharedModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.Configure<MovieDto>(
  builder.Configuration.GetSection("mockData"));

builder.Services.AddHttpClient<IMoveSearchOmdb, MoveSearchOmdb>(client =>
{
    client.BaseAddress = new Uri($"http://www.omdbapi.com/");
});

//TODO: REMOVE
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<SearchHistory>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MovieSearcher.Client._Imports).Assembly);

app.Run();
