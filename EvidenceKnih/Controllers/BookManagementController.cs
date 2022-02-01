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
using Microsoft.AspNetCore.Http;
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
        
        /// <summary>
        /// Umožňuje vygenerovat JWT
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [HttpPost(nameof(Login))]
        public ActionResult Login()
        {
            string token = _tokenAuthService.BuildToken();
            
            if (string.IsNullOrEmpty(token)) return Unauthorized();

            _logger.LogInformation("JWT vygenerován");
            return Ok(token);
        }

        /// <summary>
        /// Založí novou knihu
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <param name="bookCategory"></param>
        /// <param name="languageCategory"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CreateBookResponse), StatusCodes.Status201Created)]
        [HttpPost(nameof(CreateBook))]
        public async Task<ActionResult<CreateBookResponse>> CreateBook([FromBody] BookBaseRequest baseRequest, EnumBookCategory bookCategory, EnumLanguageCategory languageCategory)
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
            
            var book = await _bookManagment.CreateBook(bookCreateRequest);

            _logger.LogInformation("Kniha založena");
            return Created($"api/v1/{nameof(GetBook)}/{book.BookId}", book);
        }

        /// <summary>
        /// Získá knihu dle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetBookResponse), StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBook))]
        public async Task<ActionResult<GetBookResponse>> GetBook([Required] int id)
        {
            var book = await _bookManagment.GetBook(id);

            if (book.ErrorResponse.Errors.Any()) return NotFound(book.ErrorResponse);

            _logger.LogInformation("Kniha nalezena");
            return Ok(book.BookResponse);
        }
        
        /// <summary>
        /// Získá všechny knihy na skladě
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<BookResponse>), StatusCodes.Status200OK)]
        [HttpGet(nameof(GetBooksInStock))]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooksInStock()
        {
            var books = await _bookManagment.GetBooksInStock();

            if (!books.Any()) return NotFound();
            
            _logger.LogInformation("Nalezen požadovaný seznam knih");
            return Ok(books);
        }

        /// <summary>
        /// Aktualizuje informace o knize
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <param name="bookCategory"></param>
        /// <param name="languageCategory"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UpdateBookResponse), StatusCodes.Status200OK)]
        [HttpPut(nameof(UpdateBook))]
        public async Task<ActionResult<UpdateBookResponse>> UpdateBook([FromBody] BookUpdateRequest updateRequest, EnumBookCategory bookCategory, EnumLanguageCategory languageCategory)
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
            
            var response = await _bookManagment.UpdateBook(bookUpdateRequest);
            
            if (response.ErrorResponse.Errors.Any()) return NotFound(response.ErrorResponse);

            _logger.LogInformation("Kniha aktualizována");
            return Ok(response);
        }

        /// <summary>
        /// Smaže knihu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DeleteBookResponse), StatusCodes.Status200OK)]
        [HttpDelete(nameof(DeleteBook))]
        public async Task<ActionResult<DeleteBookResponse>> DeleteBook([Required] int id)
        {
            var book = await _bookManagment.DeleteBook(id);
            
            if (book.ErrorResponse.Errors.Any()) return NotFound(book.ErrorResponse);

            _logger.LogInformation("Kniha smazána");
            return Ok(book);
        }
    }
}