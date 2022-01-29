using System.Collections.Generic;

namespace Contracts.Api.Responses.Common
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
    
    public class ErrorModel
    {
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
        
        public override string ToString()
        {
            return $"{{{nameof(FieldName)}={FieldName}, {nameof(Message)}={Message}}}";
        }
    }
}