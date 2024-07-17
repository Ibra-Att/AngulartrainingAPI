using AngulartrainingAPI.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngulartrainingAPI.Helper.Token
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(User input)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFsk#vhHJVCBYHVSKDGHASVBCL");
            var tokenDescriptior = new SecurityTokenDescriptor   // this is called PayLoad
            {
                Subject = new ClaimsIdentity(new Claim[]  //her u can add your claims
                {
                        new Claim("Email",input.Email),
                        new Claim("UserId",input.Id.ToString())
                        
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                , SecurityAlgorithms.HmacSha256Signature) //hashing the token 
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }


        public static bool IsValidToken(string tokenString)  //Decode
        {
            String toke = "Bearer " + tokenString;
            var jwtEncodedString = toke.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);

            if (token.ValidTo > DateTime.UtcNow)
            {
                //claims validation
                int userID = int.Parse((token.Claims.First(c => c.Type == "UserId").Value.ToString()));


                //valid
                return true;
            }
            return false;
          
        }

    }
}
