using Application.Commons.Enums;

namespace Application.Commons.Interfaces;

public interface ICurrentUserService
{
    public string UserName { get; }
    public string UserId { get; set; }
    public bool IsAuthenticated { get; }
    public IEnumerable<string> Roles { get; set; }
    public bool HasRole(RoleEnum roleEnum);
    public bool HasRole(RoleEnum[] roleEnum);
    public bool HasRole(string role);
}
