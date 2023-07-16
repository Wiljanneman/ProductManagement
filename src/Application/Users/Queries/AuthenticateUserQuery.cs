using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Users.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Application.Users.Queries;
public class AuthenticateUserQuery: IRequest<TokenResponseVM>
{
    public UserVM Credentials { get; set; }
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, TokenResponseVM>
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IConfiguration _configuration;

        public AuthenticateUserQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration configuration)
        {
            _userMgr = userMgr;
            _hasher = hasher;
            _configuration = configuration;
        }
        public async Task<TokenResponseVM> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userMgr.FindByEmailAsync(request.Credentials.Email);
            if (user == null)
            {
                return new TokenResponseVM
                {
                    Status = 500,
                    Message = "No User Found with current credentials"
                };
            };
            if (_hasher.VerifyHashedPassword(user, user.PasswordHash, request.Credentials.Password) == PasswordVerificationResult.Success)
            {
                var roles = await _userMgr.GetClaimsAsync(user);
                string roleString = string.Join(",", roles.Where(p => p.Type == System.Security.Claims.ClaimTypes.Role).Select(p => p.Value));

                var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim("userId", user.Id),
                            new Claim("role", roleString),
                        };

                claims.AddRange(roles.Select(role => new Claim(System.Security.Claims.ClaimTypes.Role, role.Value)));

                // would be stored in azure
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                    );

                return new TokenResponseVM
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Status = 200,
                    Expiration = token.ValidTo,
                    UserId = user.Id,
                    UserName = user.UserName,

                };
            }
            else
            {
                return new TokenResponseVM
                {
                    Status = 401,
                    Message = "User does not exist or Credentials are incorrect"
                };
            }

        }
    }
}

