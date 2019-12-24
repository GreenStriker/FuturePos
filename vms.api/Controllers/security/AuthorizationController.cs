using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using vms.entity.viewModels;
using vms.utility.StaticData;

namespace vms.api.Controllers
{
    //[Route("api/authorization")]
    public class AuthorizationController : Controller
    {
        private readonly IConfiguration _config;

        public AuthorizationController(IConfiguration config)
        {
            this._config = config;
        }
        [AllowAnonymous]
        [HttpPost("/token"), Produces(MimeType.Application_Json)]
        public async Task<IActionResult> CreateToken(JwtTokenInfo request)
        {
            IActionResult response = Unauthorized();
            if (request.grant_type == "password")
            {
                if (request != null)
                {

                    request.clientId = "1";
                    request.clientName = "nayar trade";
                    request.email = "mohebbo@yahoo.com";
                    request.name = "mohebbo";
                    request.username = "1111";

                    var tokenString = await BuildToken(request);
                    response = Ok(new
                    {
                        access_token = tokenString,
                        expiration = DateTime.UtcNow.AddHours(8)
                    });
                }
            }
            else if (request.grant_type == "refresh_token")
            {
                response = await BuildRefreshToken(request.refreshtoken);
                return response;
            }




            return response;
        }
        private async Task<string> BuildToken(JwtTokenInfo request)
        {
            //var claims = new[] {
            //    new Claim(JwtRegisteredClaimNames.Sub, request.username),
            //    new Claim(JwtRegisteredClaimNames.Email, request.email),
            //    new Claim("ClientId",request.clientId,JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("Name",request.name,JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("ClientName",request.clientName,JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //   };


            var claimsdata = new[]
           {
                 new  Claim("ClientId","testClient"),
                 new Claim("Company","biTS")
           };



            //IEnumerable<Claim> claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, companyName + email, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim(JwtRegisteredClaimNames.UniqueName, userName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("UserId", userId, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("UserName", userName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("CompanyName", companyName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("CompanyId", companyId, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("Email", email, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("StorageName", storageName, JwtSecurityTokenHandler.JsonClaimTypeProperty),
            //    new Claim("PSK", "ANQH4P3WD3BBI5KE", JwtSecurityTokenHandler.JsonClaimTypeProperty)
            //};


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["origins"],
              claimsdata,
              expires: DateTime.Now.AddHours(8),
              signingCredentials: creds);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        private async Task<ActionResult> BuildRefreshToken(string token)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var handler = new JwtSecurityTokenHandler();
                var refreshToken = handler.ReadToken(token) as JwtSecurityToken;
                var claims = refreshToken.Claims.ToList();
                //claims.RemoveRange(5, 3);
                var newToken = new JwtSecurityToken(_config["Jwt:Issuer"], _config["origins"], claims, expires: DateTime.Now.AddHours(8), signingCredentials: credentials);
                return Ok(await Task.FromResult(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(newToken),
                    expiration = newToken.ValidTo
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
