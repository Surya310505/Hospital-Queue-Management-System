using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using HospitalQueueSystem.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
namespace HospitalQueueSystem.Services
{
    public class JwtService
    {
       
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config=config;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,user.Role)

            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]!));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:creds

            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}