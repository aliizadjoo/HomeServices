using App.Domain.Core.Constants;
using App.Domain.Core.Contract.CustomerAgg.Service;
using App.Domain.Core.Contract.ExpertAgg.Service;
using App.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

public class CustomUserClaimsPrincipalFactory(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    IOptions<IdentityOptions> options,
    ICustomerService _customerService,
    IExpertService _expertService
) : UserClaimsPrincipalFactory<AppUser, IdentityRole<int>>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        
        var identity = await base.GenerateClaimsAsync(user);

        var roles = await userManager.GetRolesAsync(user);

        if (roles.Contains(RoleTypes.Customer))
        {
           
            var customerId = await _customerService.GetIdByAppUserId(user.Id, default);
            if (customerId > 0)
            {
                identity.AddClaim(new Claim(CustomClaimTypes.CustomerId, customerId.ToString()));
            }
        }

        if (roles.Contains(RoleTypes.Expert))
        {
            var expertId = await _expertService.GetIdByAppUserId(user.Id, default);
            if (expertId > 0)
            {
                identity.AddClaim(new Claim(CustomClaimTypes.ExpertId, expertId.ToString()));
            }
        }

        return identity;
    }
}