using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Database
{
    /// <summary>
    /// EF entita: Sklad knihy
    /// </summary>
    public class BookStock
    {
        /// <summary>
        /// Cízy klíč na knihu
        /// </summary>
        [ForeignKey("Book")]
        public int BookStockId { get; set; }
        
        /// <summary>
        /// Počet kusů na skladě
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Odkaz na knihu
        /// One to One relationship
        /// </summary>
        public virtual Book Book { get; set; }
    }
}