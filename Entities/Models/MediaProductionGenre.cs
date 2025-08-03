using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MediaProductionGenre
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Models.MediaProduction))]
        public int MediaProductionId { get; set; }
        
        public virtual MediaProduction? MediaProduction { get; set; }

        [ForeignKey(nameof(Models.Genre))]
        public int GenreId { get; set; }

        public virtual Genre? Genre { get; set; }
    }
}
