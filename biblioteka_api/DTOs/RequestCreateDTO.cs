using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.DTOs
{
    public class RequestCreateDTO
    {
        [Required]
        public int BookId { get; set; }

    }
}
