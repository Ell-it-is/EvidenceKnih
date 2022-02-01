using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;
using Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace EvidenceKnih.Data
{
    public class BookManagment : IBookManagment
    {
        private readonly EvidenceKnihContext _context;

        public BookManagment(EvidenceKnihContext context)
        {
            _context = context;
        }

        public async Task<CreateBookResponse> CreateBook(BookCreateRequest createRequest)
        {
            var response = new CreateBookResponse();
            
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

        public async Task<IEnumerable<BookResponse>> GetBooksInStock()
        {
            var response = await _context.Books.Where(b => b.BookStock.Quantity > 0).ToListAsync();
            return response.Select(MapBookToBookResponse);
        }

        public async Task<UpdateBookResponse> UpdateBook(BookUpdateRequest updateRequest)
        {
            var response = new UpdateBookResponse();
            
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

        public async Task<DeleteBookResponse> DeleteBook(int id)
        {
            var response = new DeleteBookResponse();
            
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
        
        private BookResponse MapBookToBookResponse(Book book)
        {
            return new BookResponse()
            {
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