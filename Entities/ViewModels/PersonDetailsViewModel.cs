using Entities.Models;

namespace Entities.ViewModels
{
    public class PersonDetailsViewModel
    {
        public Person? Person { get; set; }
        public IEnumerable<MediaProduction>? MediaProductions { get; set; }
    }
}
