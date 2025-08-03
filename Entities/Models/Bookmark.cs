using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int MediaProductionId { get; set; }
    }
}
