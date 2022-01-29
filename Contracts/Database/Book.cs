using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Database.Enums;

namespace Contracts.Database
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Title { get; set; }
        
        [MaxLength(80)]
        public string Author { get; set; }
        
        [MaxLength(3000)]
        public string Description { get; set; }
        
        public int NumberOfPages { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public EnumBookCategory BookCategory { get; set; }
        
        public EnumLanguageCategory LanguageCategory { get; set; }
        
        public decimal Price { get; set; }
        
        public virtual BookStock BookStock { get; set; }
    }
}