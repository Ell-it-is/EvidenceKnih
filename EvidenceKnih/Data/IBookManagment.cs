using System.Collections.Generic;
using Contracts.Api.Requests;
using Contracts.Api.Responses;

namespace EvidenceKnih.Data
{
    public interface IBookManagment
    {
        void CreateBook(BookCreateRequest createRequest);

        BookResponse GetBook(int id);

        IEnumerable<BookResponse> GetBooks();

        void UpdateBook(BookUpdateRequest updateRequest);

        void DeleteBook(int id);
    }
}