using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Users.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;
public class RegisterUserCommand : IRequest<string>
{
    public RegisterUserVM User { get; set; }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userMgr)
        {
            _userMgr = userMgr;
            _context = context;
        }
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = _context.ApplicationUsers.Any(a => a.Email.ToLower().Trim() == request.User.Email.ToLower().Trim());
            if (existingUser)
            {
                throw new Exception("User already registered");
            }
            var user = new ApplicationUser
            {
                UserName = request.User.Email,
                Email = request.User.Email,
            };

            var password = request.User.Password;

            var result = await _userMgr.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception("User could not be registered");
            }
            return user.Id;
        }
    }
}
