using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;
using Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace EvidenceKnih.Data
{
    /// <summary>
    /// Operace pro správu databáze 'EvidenceKnih'
    /// </summary>
    public class BookManagment : IBookManagment
    {
        private readonly EvidenceKnihContext _context;

        public BookManagment(EvidenceKnihContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Založí novou knihu 
        /// Je třeba vložit alespoň jednu knihu. (Book.Quantity > 0)
        /// </summary>
        /// <param name="createRequest"></param>
        /// <returns></returns>
        public async Task<CreateBookResponse> CreateBook(BookCreateRequest createRequest)
        {
            var response = new CreateBookResponse();
            
            // Kontrola jestliže byla vložena alespoň jedna kniha.
            if (createRequest.Quantity is 0)
            {  
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(createRequest.Quantity), "Při založení knihy musí být přidána aspoň jedna kniha."));
                return response;
            }

            var book = _context.Books.Add(new Book
            {
                Title = createRequest.Title,
                Author = createRequest.Author,
                Description = createRequest.Description,
                NumberOfPages = createRequest.NumberOfPages,
                ReleaseDate = createRequest.ReleaseDate,
                Price = createRequest.Price,
                BookCategory = createRequest.BookCategory,
                LanguageCategory = createRequest.LanguageCategory,
            });
            
            book.Entity.BookStock = new BookStock()
            {
                Book = book.Entity,
                Quantity = createRequest.Quantity
            };

            await _context.SaveChangesAsync();
            
            response.BookId = book.Entity.Id;
            response.Book = MapBookToBookResponse(book.Entity);
            return response;
        }

        /// <summary>
        /// Získá knihu dle Id
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor knihy v DB</param>
        /// <returns>Knihu</returns>
        public async Task<GetBookResponse> GetBook(int id)
        {
            var response = new GetBookResponse();
            
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (book == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(id), "Kniha dle zadaného id nebyla nalezena."));
                return response;
            }
            if (book.BookStock.Quantity == 0)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(book.BookStock.Quantity), "Kniha není na skladě."));
                return response;
            }
            
            response.BookResponse = MapBookToBookResponse(book);
            return response;
        }

        /// <summary>
        /// Získá všechny knihy na skladě (Book.Quantity > 0)
        /// </summary>
        /// <returns>Seznam knih</returns>
        public async Task<IEnumerable<BookResponse>> GetBooksInStock()
        {
            var response = await _context.Books.Where(b => b.BookStock.Quantity > 0).ToListAsync();
            return response.Select(MapBookToBookResponse);
        }

        /// <summary>
        /// Aktualizuje informace o knize
        /// </summary>
        /// <param name="updateRequest">Detaily knihy</param>
        /// <returns>Aktualizovanou knihu</returns>
        public async Task<UpdateBookResponse> UpdateBook(BookUpdateRequest updateRequest)
        {
            var response = new UpdateBookResponse();
            
            // Nalezne knihu k aktualizaci
            var bookToUpdate = await _context.Books.FirstOrDefaultAsync(book => book.Id == updateRequest.Id);
            if (bookToUpdate == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(updateRequest.Id), "Kniha dle zadaného id nebyla nalezena."));
                return response;
            }
            
            bookToUpdate.Title = updateRequest.Title;
            bookToUpdate.Author = updateRequest.Author;
            bookToUpdate.Description = updateRequest.Description;
            bookToUpdate.NumberOfPages = updateRequest.NumberOfPages;
            bookToUpdate.ReleaseDate = updateRequest.ReleaseDate;
            bookToUpdate.Price = updateRequest.Price;
            bookToUpdate.BookCategory = updateRequest.BookCategory;
            bookToUpdate.LanguageCategory = updateRequest.LanguageCategory;
            bookToUpdate.BookStock.Quantity = updateRequest.Quantity;

            await _context.SaveChangesAsync();

            return response;
        }

        /// <summary>
        /// Smaže knihu
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor knihy v DB</param>
        public async Task<DeleteBookResponse> DeleteBook(int id)
        {
            var response = new DeleteBookResponse();
            
            // Nalezne knihu k smazani
            var bookToDelete = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (bookToDelete == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(id), "Kniha dle zadaného id nebyla nalezena."));
                return response;
            }

            _context.Books.Remove(bookToDelete);

            await _context.SaveChangesAsync();

            return response;
        }
        
        /// <summary>
        /// Namapuje db objekt na API response
        /// </summary>
        /// <param name="book"></param>
        /// <returns>BookResponse</returns>
        private BookResponse MapBookToBookResponse(Book book)
        {
            return new BookResponse()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                NumberOfPages = book.NumberOfPages,
                ReleaseDate = book.ReleaseDate,
                Price = book.Price,
                BookCategory = book.BookCategory,
                LanguageCategory = book.LanguageCategory,
                Quantity = book.BookStock.Quantity
            };
        }
    }
}