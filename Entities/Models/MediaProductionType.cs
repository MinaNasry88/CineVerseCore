﻿using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class MediaProductionType
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
