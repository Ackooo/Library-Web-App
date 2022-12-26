using biblioteka_api.Models;

namespace biblioteka_api.Extensions
{
    public class SeederService
    {
        private readonly BooksContext _dbContext;

        public SeederService(BooksContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {
            
            Console.WriteLine("Hello I Seed");

            //Bibliotekar.1@bibliotekar.com
            //Posetilac.1@posetilac.com

            if(_dbContext.Books.FirstOrDefault() == null) {
                var book = new Book
                {
                    Title = "The Fellowship of the Ring",
                    Author = "JRR Tolkien",
                    Available = 10
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The Two Towers",
                    Author = "JRR Tolkien",
                    Available = 7
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The Return of the King",
                    Author = "JRR Tolkien",
                    Available = 0
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The Hobbit",
                    Author = "JRR Tolkien",
                    Available = 3
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The Silmarilion",
                    Author = "JRR Tolkien",
                    Available = 2
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "A Game of Thrones",
                    Author = "GRR Martin",
                    Available = 2
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "A Clash of Kings",
                    Author = "GRR Martin",
                    Available = 2
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "A Storm of Swords",
                    Author = "GRR Martin",
                    Available = 2
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "A Feast for Crows",
                    Author = "GRR Martin",
                    Available = 1
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "A Dance with Dragons",
                    Author = "GRR Martin",
                    Available = 1
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The Winds of Winter",
                    Author = "GRR Martin",
                    Available = 1
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "The life and games of Mikhail Tal",
                    Author = "Mikhail Tal",
                    Available = 1
                };
                _dbContext.Books.Add(book);
                book = new Book
                {
                    Title = "Raven",
                    Author = "Edgar Allan Poe",
                    Available = 1
                };
                _dbContext.Books.Add(book);

                _dbContext.SaveChanges();
            }
        }
    }
}
