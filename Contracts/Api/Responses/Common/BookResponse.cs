using System;
using Contracts.Database.Enums;

namespace Contracts.Api.Responses.Common
{
    public class BookResponse
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime ReleaseDate { get; set; }
        public EnumBookCategory BookCategory { get; set; }
        public EnumLanguageCategory LanguageCategory { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}