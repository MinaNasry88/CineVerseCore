using Entities.IdentityModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<MediaProduction> MediaProductions { get; set; }
        public DbSet<Starring> Stars { get; set; }
        public DbSet<MediaProductionType> MediaProductionTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MediaProductionGenre> MediaProductionGenres { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
    }
}
