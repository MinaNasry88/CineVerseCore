using Entities.AppDbContext;
using Entities.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

// Adding Identity as a service
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 3;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


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
app.MapControllers(); // Executes the filter pipeline (action methods + filters)

app.Run();
