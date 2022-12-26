using System.ComponentModel.DataAnnotations;

namespace biblioteka_api.Models
{
    public class Book
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Available { get; set; }


    }
}
