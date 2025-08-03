using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MediaProduction
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Episodes { get; set; }
        public string? ImagePath { get; set; }

        [ForeignKey(nameof(Models.MediaProductionType))]
        public int MediaProductionTypeId { get; set; }

        public virtual MediaProductionType? MediaProductionType { get; set; }
    }
}
