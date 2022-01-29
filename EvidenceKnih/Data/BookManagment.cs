using System.Collections.Generic;
using System.Linq;
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

        public CreateBookResponse CreateBook(BookCreateRequest createRequest)
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

            _context.Stocks.Add(new Stock()
            {
                Book = book.Entity,
                Quantity = createRequest.Quantity ?? 1
            });

            _context.SaveChanges();
            
            response.BookId = book.Entity.Id;
            return response;
        }

        public GetBookResponse GetBook(int id)
        {
            var response = new GetBookResponse();
            
            var book = _context.Books.FirstOrDefault(book => book.Id == id);
            if (book == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(book), "Kniha dle zadaného id nebyla nalezena."));
                return response;
            }
            
            bool inStock = _context.Stocks.FirstOrDefault(stock => stock.Book == book)?.Quantity != 0;
            if (!inStock)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(inStock), "Kniha není na skladě."));
                return response;
            }
            
            response.BookResponse = MapBookToBookResponse(book);
            return response;
        }

        public IEnumerable<BookResponse> GetBooks()
        {
            return _context.Books.Select(MapBookToBookResponse).ToList();
        }

        public UpdateBookResponse UpdateBook(BookUpdateRequest updateRequest)
        {
            var response = new UpdateBookResponse();
            
            var bookToUpdate = _context.Books.FirstOrDefault(book => book.Id == updateRequest.Id);
            if (bookToUpdate == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(bookToUpdate), "Kniha dle zadaného id nebyla nalezena."));
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

            var stock = _context.Stocks.FirstOrDefault(stock => stock.Book == bookToUpdate);
            if (stock != null) stock.Quantity = updateRequest.Quantity ?? 1;

            _context.SaveChanges();

            return response;
        }

        public DeleteBookResponse DeleteBook(int id)
        {
            var response = new DeleteBookResponse();
            
            var bookToDelete = _context.Books.FirstOrDefault(book => book.Id == id);
            if (bookToDelete == null)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(bookToDelete), "Kniha dle zadaného id nebyla nalezena."));
                return response;
            }

            bool inStock = _context.Stocks.FirstOrDefault(stock => stock.Book == bookToDelete)?.Quantity > 0;
            if(!inStock)
            {
                response.ErrorResponse.Errors.Add(new ErrorModel(nameof(inStock), "Kniha není na skladě."));
                return response;
            }
            
            _context.Books.Remove(bookToDelete);

            _context.SaveChanges();

            return response;
        }
        
        private BookResponse MapBookToBookResponse(Book book)
        {
            var stock = _context.Stocks.FirstOrDefault(stock => stock.Book == book);

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
                Quantity = stock?.Quantity ?? 0
            };
        }
    }
}