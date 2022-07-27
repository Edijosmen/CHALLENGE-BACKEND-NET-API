using CHALLENGE_BACKEND.Moldels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CHALLENGE_BACKEND.Security
{
    public class JwtConfiguration
    {
        public static string GetToken(Usuario usuario, IConfiguration _config)
        {
            string secretkey = _config["Jwt:Secretkey"];
            string Issuer = _config["Jwt:Issuer"];
            string Audience = _config["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsuario),
                new Claim("idUsuario",usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                                issuer: Issuer,
                                audience: Audience,
                                claims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int GetTokenIduser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "idUsuario")
                    {
                        return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }
    }
}