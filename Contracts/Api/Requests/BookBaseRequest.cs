using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contracts.Database;
using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    /// <summary>
    /// Bázová třída pro požadavek knihy
    /// </summary>
    public class BookBaseRequest
    {
        /// <summary>
        /// Název knihy
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        
        /// <summary>
        /// Autor
        /// </summary>
        [MaxLength(80)]
        public string Author { get; set; }
        
        /// <summary>
        /// Popis
        /// </summary>
        [MaxLength(3000)]
        public string Description { get; set; }
        
        /// <summary>
        /// Počet stran
        /// </summary>
        public int NumberOfPages { get; set; }
        
        /// <summary>
        /// Datum vydání
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        /// <summary>
        /// Cena
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Počet kusů na skladě
        /// </summary>
        public int Quantity { get; set; }
    }
}