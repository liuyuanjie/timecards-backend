using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Timecards.Identity
{
    public static class JwtTokenGenerator
    {
        public static string Generator(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtSetting.Issuer,
                JwtSetting.Issuer,
                claims,
                expires: DateTime.Now.AddDays(JwtSetting.ExpiredDateInDay),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}