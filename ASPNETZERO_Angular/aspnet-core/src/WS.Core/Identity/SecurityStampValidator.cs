using Abp.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WS.Authorization.Roles;
using WS.Authorization.Users;
using WS.MultiTenancy;

namespace WS.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock) 
            : base(options, signInManager, systemClock)
        {
        }
    }
}