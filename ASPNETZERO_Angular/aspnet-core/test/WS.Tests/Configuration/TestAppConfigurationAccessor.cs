using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using WS.Configuration;

namespace WS.Tests.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(WSTestModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
