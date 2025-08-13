using CineVerseCore.StartUpExtensions;
using Entities.AppDbContext;
using Entities.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Identifies the action method based on route
app.UseAuthentication(); // reads the Identity cookie if found and extracts the username and userId from it
app.UseAuthorization(); // Validates access persmissions of the user
app.MapControllers(); // Executes the filter pipeline (action methods + filters)

app.Run();
