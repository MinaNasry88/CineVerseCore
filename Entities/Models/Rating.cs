using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public float Value { get; set; }
        public int Votes { get; set; }
        
        [ForeignKey(nameof(Models.MediaProduction))]
        public int MediaProductionId { get; set; }
        public virtual MediaProduction? MediaProduction { get; set; }
    }
}
