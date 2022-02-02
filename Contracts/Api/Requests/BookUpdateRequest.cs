using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Api.Responses;
using Contracts.Database.Enums;

namespace Contracts.Api.Requests
{
    /// <summary>
    /// Požadavek knihy pro její aktualizaci
    /// </summary>
    public class BookUpdateRequest : BookBaseRequest
    {
        /// <summary>
        /// Jednoznačný identifikátor knihy v DB
        /// </summary>
        [Required]
        public int Id { get; set; }
        
        /// <summary>
        /// Kategorie
        /// </summary>
        public EnumBookCategory BookCategory { get; set; }

        /// <summary>
        /// Jazyk
        /// </summary>
        public EnumLanguageCategory LanguageCategory { get; set; }
    }
}