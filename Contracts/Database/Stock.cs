using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Database
{
    public class Stock
    {
        [Key, ForeignKey("Book")]
        public int BookStockId { get; set; }

        public Book Book { get; set; }
        
        public int Quantity { get; set; }
    }
}