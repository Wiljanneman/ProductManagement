using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Common;
public class TokenResponseVM
{
    public string Token { get; set; }
    public int Status { get; set; }
    public DateTime Expiration { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public string ProfileUrl { get; set; }
    public string LastName { get; set; }
    public string CellNumber { get; set; }
    public string Message { get; set; }
}

