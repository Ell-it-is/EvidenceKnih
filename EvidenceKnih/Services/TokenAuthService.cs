using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EvidenceKnih.Services
{
	/// <summary>
	/// Obsahuje metody pro práci s JWT
	/// </summary>
	public class TokenAuthService : ITokenAuthService
	{
		private const string SecretKey = "Jwt:SecretKey";
		private const string Issuer = "Jwt:Issuer";
		private const string Audience = "Jwt:Audience";

		private readonly IConfiguration _configuration;

		public TokenAuthService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Vytvoří nový token
		/// </summary>
		/// <returns>Jwt</returns>
		public string BuildToken()
		{
			// secretKey - secret known only to the issuer, used for hashing
			var secretKey = _configuration.GetValue<string>(SecretKey);

			// issuer - Who issued the Jwt
			var issuer = _configuration.GetValue<string>(Issuer);
			
			// audience - Who is the Jwt intended for
			var audience = _configuration.GetValue<string>(Audience);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer, 
				audience,
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}