using Xunit;

namespace WS.Tests
{
    public sealed class MultiTenantTheoryAttribute : TheoryAttribute
    {
        public MultiTenantTheoryAttribute()
        {
            if (!WSConsts.MultiTenancyEnabled)
            {
                Skip = "MultiTenancy is disabled.";
            }
        }
    }
}