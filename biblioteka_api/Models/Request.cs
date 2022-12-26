using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteka_api.Models
{
    public class Request
    {
        public int Id { get; set; }
        [ForeignKey("Books")]
        public int BookId { get; set; }
        [ForeignKey("Users")]
       public string UserId { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public int State { get; set; }//0 pending   // 1 accepted   // 2 returned // 3 //denied                                
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
