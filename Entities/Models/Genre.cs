using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string? GenreName { get; set; }
    }
}
