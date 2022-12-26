using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.DTOs
{
    public class UserCreateDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
