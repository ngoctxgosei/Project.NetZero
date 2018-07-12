using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using WS.Configuration;
using WS.EntityFrameworkCore;
using WS.Migrator.DependencyInjection;

namespace WS.Migrator
{
    [DependsOn(typeof(WSEntityFrameworkCoreModule))]
    public class WSMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WSMigratorModule(WSEntityFrameworkCoreModule abpZeroTemplateEntityFrameworkCoreModule)
        {
            abpZeroTemplateEntityFrameworkCoreModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(WSMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                WSConsts.ConnectionStringName
                );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}