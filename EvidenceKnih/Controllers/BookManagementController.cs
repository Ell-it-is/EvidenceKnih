using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Api.Responses;
using EvidenceKnih.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITokenAuthService _tokenAuthService;

        public BookManagementController(ILogger<BookManagementController> logger, 
            ITokenAuthService tokenAuthService)
        {
            _logger = logger;
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
        public ActionResult<int> CreateBook()
        {
            return 1;
        }

        [HttpGet(nameof(GetBook))]
        public ActionResult<BookResponse> GetBook(int id)
        {
            return new BookResponse();
        }
        
        [HttpGet(nameof(GetBooks))]
        public ActionResult<IEnumerable<BookResponse>> GetBooks()
        {
            return new List<BookResponse>();
        }

        [HttpPut(nameof(UpdateBook))]
        public ActionResult<int> UpdateBook()
        {
            return 1;
        }

        [HttpDelete(nameof(DeleteBook))]
        public ActionResult<int> DeleteBook()
        {
            return Ok(1);
        }
    }
}