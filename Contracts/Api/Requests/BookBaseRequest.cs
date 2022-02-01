using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contracts.Database;
using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    public class BookBaseRequest
    {
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(80)]
        public string Author { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}