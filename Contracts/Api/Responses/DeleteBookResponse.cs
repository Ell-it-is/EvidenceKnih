using Contracts.Api.Responses.Common;

namespace Contracts.Api.Responses
{
    /// <summary>
    /// Odpověď na smazání knihy
    /// </summary>
    public class DeleteBookResponse
    {
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}