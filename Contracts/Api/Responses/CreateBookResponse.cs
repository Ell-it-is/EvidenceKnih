using Contracts.Api.Responses.Common;

namespace Contracts.Api.Responses
{
    public class CreateBookResponse
    {
        public int BookId { get; set; }
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}