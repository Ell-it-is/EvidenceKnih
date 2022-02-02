using Contracts.Api.Responses.Common;

namespace Contracts.Api.Responses
{
    /// <summary>
    /// Odpověď na aktualizaci knihy
    /// </summary>
    public class UpdateBookResponse
    {
        /// <summary>
        /// Seznam chyb
        /// </summary>
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}