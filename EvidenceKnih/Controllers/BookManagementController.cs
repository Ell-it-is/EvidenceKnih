using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvidenceKnih.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class BookManagementController : ControllerBase
    {

        private readonly ILogger<BookManagementController> _logger;

        public BookManagementController(ILogger<BookManagementController> logger)
        {
            _logger = logger;
        }

        [HttpPost(nameof(CreateBook))]
        public int CreateBook()
        {
            return 1;
        }

        [HttpGet(nameof(GetBook))]
        public BookResponse GetBook(int id)
        {
            return new BookResponse();
        }
        
        [HttpGet(nameof(GetBooks))]
        public IEnumerable<BookResponse> GetBooks()
        {
            return new List<BookResponse>();
        }

        [HttpPut(nameof(UpdateBook))]
        public int UpdateBook()
        {
            return 1;
        }

        [HttpDelete(nameof(DeleteBook))]
        public int DeleteBook()
        {
            return 1;
        }
    }
}