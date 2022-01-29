using System;

namespace Contracts.Database
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BookCategoryId { get; set; }
        public int LanguageCategoryId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}