using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    /// <summary>
    /// Požadavek knihy pro její založení
    /// </summary>
    public class BookCreateRequest : BookBaseRequest
    {
        /// <summary>
        /// kategorie 
        /// </summary>
        public EnumBookCategory BookCategory { get; set; }
        
        /// <summary>
        /// Jazyk
        /// </summary>
        public EnumLanguageCategory LanguageCategory { get; set; }
    }
}