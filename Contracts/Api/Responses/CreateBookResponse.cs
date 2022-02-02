using Contracts.Api.Responses.Common;
using Contracts.Database;

namespace Contracts.Api.Responses
{
    /// <summary>
    /// Odpověď na založení nové knihy
    /// </summary>
    public class CreateBookResponse
    {
        /// <summary>
        /// Jednoznačný identifikátor knihy v DB
        /// </summary>
        public int BookId { get; set; }
        
        /// <summary>
        /// Detaily knihy
        /// </summary>
        public BookResponse Book { get; set; }
        
        /// <summary>
        /// Seznam chyb
        /// </summary>
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}