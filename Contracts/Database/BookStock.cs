using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Database
{
    public class BookStock
    {
        [ForeignKey("Book")]
        public int BookStockId { get; set; }
        
        public int Quantity { get; set; }
        
        public virtual Book Book { get; set; }
    }
}