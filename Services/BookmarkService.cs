using Entities.AppDbContext;
using Entities.IdentityModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookmarkService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddBookMark(int id, string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (_db.Bookmarks.Where(b => b.UserId == user!.Id).Any(b => b.MediaProductionId == id))
            {
                return;
            }
            else
            { 
                Bookmark bookmark = new Bookmark()
                {
                    UserId = user!.Id,
                    MediaProductionId = id
                };

                await _db.Bookmarks.AddAsync(bookmark);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteBookMark(int id, string userName)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(userName);

            Bookmark bookmark = await _db.Bookmarks.Where(b => b.UserId == user!.Id).Where(b => b.MediaProductionId == id).SingleAsync();

            _db.Bookmarks.Remove(bookmark);
            await _db.SaveChangesAsync();
        }
    }
}
