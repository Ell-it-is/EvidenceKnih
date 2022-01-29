using System.Collections.Generic;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;

namespace EvidenceKnih.Data
{
    public interface IBookManagment
    {
        CreateBookResponse CreateBook(BookCreateRequest createRequest);

        GetBookResponse GetBook(int id);

        IEnumerable<BookResponse> GetBooksInStock();

        UpdateBookResponse UpdateBook(BookUpdateRequest updateRequest);

        DeleteBookResponse DeleteBook(int id);
    }
}