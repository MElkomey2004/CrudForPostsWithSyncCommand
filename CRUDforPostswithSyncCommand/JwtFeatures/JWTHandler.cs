using CRUDforPostswithSyncCommand.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDforPostswithSyncCommand.JwtFeatures
{
	public class JWTHandler
	{
		private readonly IConfiguration _configuration;
		private readonly IConfiguration _jwtSettings;
        public JWTHandler(IConfiguration configration)
        {
            _configuration = configration;
			_jwtSettings = _configuration.GetSection("JWTSettings");
        }

		public string createToken(User user, IList<string> roles)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = GetClaims(user, roles);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);


			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

		}


		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);

			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private List<Claim> GetClaims(User user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name , user.UserName)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			return claims;
		}
		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				issuer: _jwtSettings["validIssuer"],
				audience: _jwtSettings["validAudience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
				signingCredentials: signingCredentials
			);

			return tokenOptions;
		}
	}
}
