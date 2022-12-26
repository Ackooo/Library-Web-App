using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.DTOs
{
    public class UserLoginDTO
    {
        
        [EmailAddress] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Type { get; set; }
    }
}
