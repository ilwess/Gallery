using Domain.Models;
using BLL.Abstract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TokenAuthenticateService : IAuthenticateService
    {
        private readonly IUserManagementService userManagement;
        private readonly TokenManagement tokenManagement;

        public TokenAuthenticateService(
            IUserManagementService userManagement,
            IOptions<TokenManagement> tokenManagement)
        {
            this.userManagement = userManagement;
            this.tokenManagement = tokenManagement.Value;
        }

        public bool IsAuthenticate(TokenRequest request, out string token)
        {
            token = string.Empty;
            if (!userManagement
                .IsValidUser(
                request.UserName,
                request.Password)) return false;

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8
                .GetBytes(tokenManagement.Secret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                tokenManagement.Issuer,
                tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(tokenManagement.AccessExpiration),
                signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
