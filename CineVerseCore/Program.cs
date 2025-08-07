using Entities.AppDbContext;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMediaProductionsGetterService, MediaProductionsGetterService>();
builder.Services.AddScoped<IMoviesGetterService, MoviesGetterService>();
builder.Services.AddScoped<ITvShowsGetterService, TvShowsGetterService>();
builder.Services.AddScoped<IStarsGetterService, StarsGetterService>();
builder.Services.AddScoped<IWritersGetterService, WritersGetterService>();
builder.Services.AddScoped<IDirectorsGetterService, DirectorsGetterService>();
builder.Services.AddScoped<IMediaProductionGenresGetterService, MediaProductionGenresGetterService>();
builder.Services.AddScoped<IMediaProductionRatingGetterService, MediaProductionRatingGetterService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
