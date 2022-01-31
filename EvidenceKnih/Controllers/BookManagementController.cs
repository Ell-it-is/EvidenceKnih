using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Api.Responses.Common;
using Contracts.Database.Enums;
using EvidenceKnih.Data;
using EvidenceKnih.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvidenceKnih.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/")]
    public class BookManagementController : ControllerBase
    {
        private readonly ILogger<BookManagementController> _logger;
        private readonly IBookManagment _bookManagment;
        private readonly ITokenAuthService _tokenAuthService;

        public BookManagementController(
            ILogger<BookManagementController> logger, 
            IBookManagment bookManagment,
            ITokenAuthService tokenAuthService)
        {
            _logger = logger;
            _bookManagment = bookManagment;
            _tokenAuthService = tokenAuthService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult Login()
        {
            string token = _tokenAuthService.BuildToken();
            
            if (string.IsNullOrEmpty(token)) return Unauthorized();

            return Ok(token);
        }

        [HttpPost(nameof(CreateBook))]
        public ActionResult CreateBook([FromBody] BookBaseRequest baseRequest, EnumBookCategory bookCategory, EnumLanguageCategory languageCategory)
        {
            var bookCreateRequest = new BookCreateRequest()
            {
                Title = baseRequest.Title,
                Author = baseRequest.Author,
                Description = baseRequest.Description,
                NumberOfPages = baseRequest.NumberOfPages,
                ReleaseDate = baseRequest.ReleaseDate,
                Price = baseRequest.NumberOfPages,
                Quantity = baseRequest.Quantity,
                BookCategory = bookCategory,
                LanguageCategory = languageCategory,
            };
            
            var book = _bookManagment.CreateBook(bookCreateRequest);

            return Created($"api/v1/getBook/{book.BookId}", book.BookId);
        }

        [HttpGet(nameof(GetBook))]
        public ActionResult<CreateBookResponse> GetBook([Required] int id)
        {
            var book = _bookManagment.GetBook(id);

            if (book.ErrorResponse.Errors.Any()) return NotFound(book.ErrorResponse);

            return Ok(book.BookResponse);
        }
        
        [HttpGet(nameof(GetBooksInStock))]
        public ActionResult<IEnumerable<BookResponse>> GetBooksInStock()
        {
            var books = _bookManagment.GetBooksInStock();

            if (!books.Any()) return NotFound();
            
            return Ok(books);
        }

        [HttpPut(nameof(UpdateBook))]
        public ActionResult UpdateBook([FromBody] BookUpdateRequest updateRequest, EnumBookCategory bookCategory, EnumLanguageCategory languageCategory)
        {
            var bookUpdateRequest = new BookUpdateRequest()
            {
                Id = updateRequest.Id,
                Title = updateRequest.Title,
                Author = updateRequest.Author,
                Description = updateRequest.Description,
                NumberOfPages = updateRequest.NumberOfPages,
                ReleaseDate = updateRequest.ReleaseDate,
                Price = updateRequest.NumberOfPages,
                Quantity = updateRequest.Quantity,
                BookCategory = bookCategory,
                LanguageCategory = languageCategory
            };
            
            var response = _bookManagment.UpdateBook(bookUpdateRequest);
            
            if (response.ErrorResponse.Errors.Any()) return NotFound(response.ErrorResponse);

            return Ok("Kniha aktualizována.");
        }

        [HttpDelete(nameof(DeleteBook))]
        public ActionResult DeleteBook([Required] int id)
        {
            var book = _bookManagment.DeleteBook(id);
            
            if (book.ErrorResponse.Errors.Any()) return NotFound(book.ErrorResponse);

            return Ok("Kniha smazána.");
        }
    }
}