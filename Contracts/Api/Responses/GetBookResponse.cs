using Contracts.Api.Responses.Common;

namespace Contracts.Api.Responses
{
    /// <summary>
    /// Odpověď na úspěšné nalezení knihy
    /// </summary>
    public class GetBookResponse
    {
        /// <summary>
        /// Detail knihy
        /// </summary>
        public BookResponse BookResponse { get; set; }

        /// <summary>
        /// Seznam chyb
        /// </summary>
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}