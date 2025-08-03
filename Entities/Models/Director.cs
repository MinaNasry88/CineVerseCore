using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Models.Person))]
        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }

        [ForeignKey(nameof(Models.MediaProduction))]
        public int MediaProductionId { get; set; }
        public virtual MediaProduction? MediaProduction { get; set; }
    }
}
