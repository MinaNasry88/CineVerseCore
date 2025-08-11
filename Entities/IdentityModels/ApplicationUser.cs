﻿using Microsoft.AspNetCore.Identity;

namespace Entities.IdentityModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
