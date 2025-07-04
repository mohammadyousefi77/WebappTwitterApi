using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebappTwitterApi.Data.Entity
{
    public class User:IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(150)]
        public string ProfileImagePath { get; set; } = string.Empty;
    }
}
