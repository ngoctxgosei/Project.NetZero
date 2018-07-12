using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WS.Configuration;
using WS.Web;

namespace WS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class WSDbContextFactory : IDesignTimeDbContextFactory<WSDbContext>
    {
        public WSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            WSDbContextConfigurer.Configure(builder, configuration.GetConnectionString(WSConsts.ConnectionStringName));

            return new WSDbContext(builder.Options);
        }
    }
}