using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    public class BookCreateRequest : BookBaseRequest
    {
        public EnumBookCategory BookCategory { get; set; }
        
        public EnumLanguageCategory LanguageCategory { get; set; }
    }
}