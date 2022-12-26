using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public int UserIssuedNumber { get; set; }
        public DateTime? DateOfReturn { get; set; } 
        public int State { get; set; }
    }
}
