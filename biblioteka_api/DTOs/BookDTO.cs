﻿namespace biblioteka_api.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Available { get; set; }
    }
}
