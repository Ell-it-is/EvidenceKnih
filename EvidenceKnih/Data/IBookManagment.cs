using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;

namespace EvidenceKnih.Data
{
    /// <summary>
    /// Rozhraní pro správu databáze 'EvidenceKnih'
    /// </summary>
    public interface IBookManagment
    {
        /// <summary>
        /// Založí novou knihu
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        Task<CreateBookResponse> CreateBook(BookCreateRequest createRequest);

        /// <summary>
        /// Získá knihu dle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetBookResponse> GetBook(int id);

        /// <summary>
        /// Získá všechny knihy na skladě
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BookResponse>> GetBooksInStock();

        /// <summary>
        /// Aktualizuje informace o knize
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        Task<UpdateBookResponse> UpdateBook(BookUpdateRequest updateRequest);

        /// <summary>
        /// Smaže knihu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeleteBookResponse> DeleteBook(int id);
    }
}