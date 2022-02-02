using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Database.Enums;

namespace Contracts.Database
{
    /// <summary>
    /// EF entita: Kniha
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Jednoznačný identifikátor knihy v DB
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
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
        /// Odkaz na sklad knihy.
        /// One to One relationship
        /// </summary>
        public virtual BookStock BookStock { get; set; }
    }
}