using System;
using Contracts.Database.Enums;

namespace Contracts.Api.Responses.Common
{
    /// <summary>
    /// Detail knihy
    /// </summary>
    public class BookResponse
    {
        /// <summary>
        /// Jednoznačný identifikátor knihy v DB
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Název knihy
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Autor
        /// </summary>
        public string Author { get; set; }
        
        /// <summary>
        /// Popis
        /// </summary>
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
        /// Kategorie
        /// </summary>
        public EnumBookCategory BookCategory { get; set; }
        
        
        /// <summary>
        /// Jazyk
        /// </summary>
        public EnumLanguageCategory LanguageCategory { get; set; }
        
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