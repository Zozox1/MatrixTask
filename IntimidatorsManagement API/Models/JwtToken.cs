using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
    public class JwtToken : IJwtToken
    {

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName )
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
         /*   var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "MatrixTask",
                Audience = "Anton",
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            */
            var JWToken = new JwtSecurityToken(
     issuer: "http://localhost:45092/",
     audience: "http://localhost:45092/",
     claims: claims,
     notBefore: new DateTimeOffset(DateTime.Now).DateTime,
     expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
     //Using HS256 Algorithm to encrypt Token
     signingCredentials: creds
 );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(JWToken);
            return token;
        }
    }
}
