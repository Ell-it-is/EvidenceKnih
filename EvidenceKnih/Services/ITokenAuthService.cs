namespace EvidenceKnih.Services
{
    /// <summary>
    /// Rozhraní pro práci s JWT
    /// </summary>
    public interface ITokenAuthService
    {
        /// <summary>
        /// Vytvoří nový token
        /// </summary>
        /// <returns></returns>
        string BuildToken();
    }
}