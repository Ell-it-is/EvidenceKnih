using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;

namespace EvidenceKnih.Data
{
    public interface IBookManagment
    {
        Task<CreateBookResponse> CreateBook(BookCreateRequest createRequest);

        Task<GetBookResponse> GetBook(int id);

        Task<IEnumerable<BookResponse>> GetBooksInStock();

        Task<UpdateBookResponse> UpdateBook(BookUpdateRequest updateRequest);

        Task<DeleteBookResponse> DeleteBook(int id);
    }
}