using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Starring
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Models.Person))]
        public int PerformerId { get; set; }
        public virtual Person? Performer { get; set; }

        [ForeignKey(nameof(Models.MediaProduction))]
        public int MediaProductionId { get; set; }
        public virtual MediaProduction? MediaProduction { get; set; }
    }
}
