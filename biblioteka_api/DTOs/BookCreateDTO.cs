using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.DTOs
{
    public class BookCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public int Available { get; set; }
    }
}
