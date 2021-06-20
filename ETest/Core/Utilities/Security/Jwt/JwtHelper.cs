using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Token.Abstract;
using Core.Utilities.Security.Token.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper:ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        public IConfiguration Configuration { get; }
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateAccessToken(UserHelperPartialDto userHelperPartialDto)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, userHelperPartialDto, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return new AccessToken
            {
                Expiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                Token = jwtSecurityTokenHandler.WriteToken(jwt)
            };
        }
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserHelperPartialDto userHelperPartialDto,
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(userHelperPartialDto),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> SetClaims(UserHelperPartialDto userHelperPartialDto)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(userHelperPartialDto.Id.ToString());
            claims.AddUsername(userHelperPartialDto.Username);
            claims.AddName($"{userHelperPartialDto.FullName}");
            claims.AddRoles(userHelperPartialDto.Roles.ToList().Select(role => role).ToArray());
            return claims;
        }
    }
}