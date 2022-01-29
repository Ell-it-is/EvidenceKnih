using System.Collections.Generic;
using System.Linq;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Database;

namespace EvidenceKnih.Data
{
    public class BookManagment : IBookManagment
    {
        private readonly EvidenceKnihContext _context;

        public BookManagment(EvidenceKnihContext context)
        {
            _context = context;
        }

        public void CreateBook(BookCreateRequest createRequest)
        {
            _context.Books.Add(new Book
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

            _context.SaveChanges();
        }

        public BookResponse GetBook(int id)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == id);

            return book == null ? null : MapBookToBookResponse(book);
        }

        public IEnumerable<BookResponse> GetBooks()
        {
            return _context.Books.Select(MapBookToBookResponse).ToList();
        }

        public void UpdateBook(BookUpdateRequest updateRequest)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(book => book.Id == updateRequest.Id);
            if (bookToUpdate == null) return;
            
            bookToUpdate.Title = updateRequest.Title;
            bookToUpdate.Author = updateRequest.Author;
            bookToUpdate.Description = updateRequest.Description;
            bookToUpdate.NumberOfPages = updateRequest.NumberOfPages;
            bookToUpdate.ReleaseDate = updateRequest.ReleaseDate;
            bookToUpdate.Price = updateRequest.Price;
            bookToUpdate.BookCategory = updateRequest.BookCategory;
            bookToUpdate.LanguageCategory = updateRequest.LanguageCategory;

            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var bookToDelete = _context.Books.FirstOrDefault(book => book.Id == id);
            if (bookToDelete == null) return;

            _context.Books.Remove(bookToDelete);

            _context.SaveChanges();
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
            };
        }
    }
}