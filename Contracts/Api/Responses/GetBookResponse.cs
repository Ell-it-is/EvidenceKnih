using Contracts.Api.Responses.Common;

namespace Contracts.Api.Responses
{
    public class GetBookResponse
    {
        public BookResponse BookResponse { get; set; }

        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}