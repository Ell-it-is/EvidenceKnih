using System.Collections.Generic;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Database.Enums;

namespace EvidenceKnih.Services
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