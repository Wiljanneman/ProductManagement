using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Common;
public class UserVM: IMapFrom<ApplicationUser>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
