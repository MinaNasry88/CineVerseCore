using Entities.IdentityModels;
using Entities.Models;

namespace Entities.ViewModels
{
    public class UserProfileViewModel
    {
        public ApplicationUser? User { get; set; }
        public IEnumerable<MediaProduction>? BookmarkedMediaProductions { get; set; }
    }
}
