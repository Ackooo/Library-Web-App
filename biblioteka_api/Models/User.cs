using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.Models
{
    public class User : IdentityUser
    {    
        public string? Name { get; set; }
        [Range(0,1)]
        public int Type { get; set; } //0 - librarian // 1- visitor

        public int IssuedNumber { get; set; }
    }
}
