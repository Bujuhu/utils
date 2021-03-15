using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Utils
{
    public class JwtIssuerService
    {
        private readonly JwtOptions _options;
        private readonly SigningCredentials _credentials;


        public JwtIssuerService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _credentials = _options.GetCredentials();
        }

        public string IssueToken(ClaimsIdentity idendtity)
        {
             var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: AddDefaultSecurityClaims(idendtity).Claims,
                expires: DateTime.UtcNow + _options.Duration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _credentials
            ) ;

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity AddDefaultSecurityClaims(ClaimsIdentity idendtity)
        {
            idendtity.AddClaims(new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.UtcNow.ToUnixEpoch().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            });
            return idendtity;
        }

        public static ClaimsIdentity ClaimIdentityNameRole(string id, string role)
        {
            Claim[] claims = new[] {
                new Claim(ClaimTypes.Name, id)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

            return claimsIdentity;

        }

    }
    public static class JwtBearerHook
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(options => {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Audience = jwtOptions.Audience;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = jwtOptions.GetCertificate(),
                    RequireExpirationTime = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/ws")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });

            services.AddSingleton<JwtIssuerService, JwtIssuerService>();
        }
    }
}