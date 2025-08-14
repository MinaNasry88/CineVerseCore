using CineVerseCore.Middleware;
using CineVerseCore.StartUpExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Identifies the action method based on route
app.UseAuthentication(); // reads the Identity cookie if found and extracts the username and userId from it
app.UseAuthorization(); // Validates access persmissions of the user
app.MapControllers(); // Executes the filter pipeline (action methods + filters)

app.Run();
