using System;
using Contracts.Database;

namespace Contracts.Api.Requests
{
    public class BookCreateRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime ReleaseDate { get; set; } //(I could add available property for books that are not yet released)
        public EnumBookCategory BookCategory { get; set; }
        public EnumLanguageCategory LanguageCategory { get; set; }
        public decimal Price { get; set; }
    }
}