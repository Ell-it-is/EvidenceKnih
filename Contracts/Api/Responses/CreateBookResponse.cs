using Contracts.Api.Responses.Common;
using Contracts.Database;

namespace Contracts.Api.Responses
{
    public class CreateBookResponse
    {
        public int BookId { get; set; }
        public BookResponse Book { get; set; }
        public ErrorResponse ErrorResponse { get; set; } = new();
    }
}