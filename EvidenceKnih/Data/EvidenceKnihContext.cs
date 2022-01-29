using Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace EvidenceKnih.Data
{
    public class EvidenceKnihContext : DbContext
    {
        public EvidenceKnihContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookStock> Stocks { get; set; }
    }
}