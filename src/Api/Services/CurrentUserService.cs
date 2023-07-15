using System.Security.Claims;
using Application.Commons.Enums;
using Application.Commons.Extensions;
using Application.Commons.Interfaces;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) : this(httpContextAccessor.HttpContext)
    {

    }

    public CurrentUserService(HttpContext context)
    {
        UserName = context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        IsAuthenticated = UserName != null;
        UserId = context?.User?.FindFirstValue("userId");
        Roles = context?.User?.FindAll(ClaimTypes.Role).Select(c => c.Value);

    }

    public string UserId { get; set; }

    public string UserName { get; }
    public IEnumerable<string> Roles { get; set; }

    public bool IsAuthenticated { get; }

    public bool HasRole(RoleEnum roleEnum)
    {
        return HasRole(roleEnum.GetDescription());
    }
    public bool HasRole(RoleEnum[] roleEnums)
    {
        return roleEnums.Any(x => HasRole(x.GetDescription()));
    }

    public bool HasRole(string role)
    {
        return Roles?.Any(x => x.ToLower() == role.ToLower()) ?? false;
    }
}