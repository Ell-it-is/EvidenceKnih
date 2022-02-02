using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EvidenceKnih.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            // Some logic to handle specific exceptions
            var errorMessage = context.Exception is ArgumentException 
                ? "ArgumentException occurred" 
                : "Some unknown error occurred";

            // Maybe, logging the exception
            _logger.LogError(context.Exception, errorMessage);

            // Returning response
            context.Result = new BadRequestResult();
        }
    }
}