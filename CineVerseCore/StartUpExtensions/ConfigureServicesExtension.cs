using Entities.AppDbContext;
using Entities.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

namespace CineVerseCore.StartUpExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddScoped<IMediaProductionsGetterService, MediaProductionsGetterService>();
            services.AddScoped<IMoviesGetterService, MoviesGetterService>();
            services.AddScoped<ITvShowsGetterService, TvShowsGetterService>();
            services.AddScoped<IStarsGetterService, StarsGetterService>();
            services.AddScoped<IWritersGetterService, WritersGetterService>();
            services.AddScoped<IDirectorsGetterService, DirectorsGetterService>();
            services.AddScoped<IMediaProductionGenresGetterService, MediaProductionGenresGetterService>();
            services.AddScoped<IMediaProductionRatingGetterService, MediaProductionRatingGetterService>();
            services.AddScoped<IBookmarkService, BookmarkService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Adding Identity as a service
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

            services.AddAuthorization(options => {
                // enforces authorization policy (user must be authenticated) for all the action methods
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            return services;
        }
    }
}
