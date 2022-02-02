using Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace EvidenceKnih.Data
{
    public class EvidenceKnihContext : DbContext
    {
        public EvidenceKnihContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Knihy
        /// </summary>
        public DbSet<Book> Books { get; set; }
        
        /// <summary>
        /// Knihy na skladě
        /// </summary>
        public DbSet<BookStock> Stocks { get; set; }
    }
}