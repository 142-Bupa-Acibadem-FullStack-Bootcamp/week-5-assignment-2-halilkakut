using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Northwind.Entity.DataTransferObject;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.BusinessLayer
{
    public class TokenManager
    {
        IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Token Üretileck Method
        public string CreateAccessToken(DtoLoginUser user)
        {
            //claims oluşturma

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserCode),
                new Claim(JwtRegisteredClaimNames.Jti,user.UserID.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            //claims roller

            var claimsRoleList = new List<Claim>
            {
                new Claim("role","Admin"),
                //new Claim("role2","")
            };

            //security key ile simetrik almak

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            // Şifrelenmiş kimlik oluşturma

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Token Ayarları yapıyoruz
            var token = new JwtSecurityToken
                (
                    issuer:_configuration["Tokens:Issuer"],//token dağıtıcı url
                    audience:_configuration["Tokens:Issuer"], //erişilebilecek api ler
                    expires:DateTime.Now.AddMinutes(5), //token süresi 5 dakika
                    notBefore: DateTime.Now, // token üretildikten sonra ne kadar süre sonra devreye girsin
                    signingCredentials:cred, //kimik verdik
                    claims:claimsIdentity.Claims //
                );


            //Token oluşturma sınıfı ile bir örnek alıp token üretiyoruz

            var tokenHandler = new { token = new JwtSecurityTokenHandler().WriteToken(token) };
            return tokenHandler.token;
        }
    }
}
