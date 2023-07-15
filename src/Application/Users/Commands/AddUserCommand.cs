using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commons.Interfaces;
using Application.Users.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;
public class AddUserCommand: IRequest
{
    public UserVM User { get; set; }

    public class Handler : IRequestHandler<AddUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userMgr;

        public Handler(IApplicationDbContext context, UserManager<ApplicationUser> userMgr)
        {
            _userMgr = userMgr;
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.User.UserName,
                Email = request.User.Email,
            };

            var password = request.User.Password;

            var result = await _userMgr.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // User created successfully
            }
            else
            {
                // User creation failed, handle the error
            }

            return Unit.Value;
        }
    }
}
