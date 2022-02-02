using System.Collections.Generic;

namespace Contracts.Api.Responses.Common
{
    /// <summary>
    /// Seznam chyb
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// List chyb
        /// </summary>
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
    
    /// <summary>
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// </summary>
        /// <param name="fieldName">K jakému objektu se chyba vztahuje</param>
        /// <param name="message">Zpráva o chybě</param>
        public ErrorModel(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }
        
        /// <summary>
        /// Pole, které vyvolalo chybu
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Text chyby
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// </summary>
        public override string ToString()
        {
            return $"{{{nameof(FieldName)}={FieldName}, {nameof(Message)}={Message}}}";
        }
    }
}