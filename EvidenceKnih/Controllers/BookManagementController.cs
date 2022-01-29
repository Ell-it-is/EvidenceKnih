using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Api.Requests;
using Contracts.Api.Responses;
using Contracts.Database.Enums;
using EvidenceKnih.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvidenceKnih.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                BookCategory = bookCategory,
                LanguageCategory = languageCategory
            };
            
            _bookManagment.CreateBook(bookCreateRequest);

            return Ok();
        }

        [HttpGet(nameof(GetBook))]
        public ActionResult<BookResponse> GetBook(int id)
        {
            var book = _bookManagment.GetBook(id);

            return Ok(book);
        }
        
        [HttpGet(nameof(GetBooks))]
        public ActionResult<IEnumerable<BookResponse>> GetBooks()
        {
            var books = _bookManagment.GetBooks();

            return Ok(books);
        }

        [HttpPut(nameof(UpdateBook))]
        public ActionResult UpdateBook([FromBody] BookBaseRequest baseRequest, EnumBookCategory bookCategory, EnumLanguageCategory languageCategory)
        {
            var bookUpdateRequest = new BookUpdateRequest()
            {
                Title = baseRequest.Title,
                Author = baseRequest.Author,
                Description = baseRequest.Description,
                NumberOfPages = baseRequest.NumberOfPages,
                ReleaseDate = baseRequest.ReleaseDate,
                Price = baseRequest.NumberOfPages,
                BookCategory = bookCategory,
                LanguageCategory = languageCategory
            };
            
            _bookManagment.UpdateBook(bookUpdateRequest);

            return Ok();
        }

        [HttpDelete(nameof(DeleteBook))]
        public ActionResult DeleteBook(int id)
        {
            _bookManagment.DeleteBook(id);

            return Ok();
        }
    }
}