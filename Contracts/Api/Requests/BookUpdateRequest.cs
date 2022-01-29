using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Api.Responses;
using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    public class BookUpdateRequest : BookBaseRequest
    {
        public int Id { get; set; }
        
        public EnumBookCategory BookCategory { get; set; }

        public EnumLanguageCategory LanguageCategory { get; set; }
    }
}