using Entities.Models;

namespace Entities.ViewModels
{
    public class MediaProductionDetailsViewModel
    {
        public MediaProduction? MediaProduction { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
        public Rating? Rating { get; set; }
        public IEnumerable<Person>? Stars { get; set; }
        public IEnumerable<Person>? Writers { get; set; }
        public IEnumerable<Person>? Directors { get; set; }
    }
}
