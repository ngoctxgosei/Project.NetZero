using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WS.Authorization.Roles;
using WS.Authorization.Users;
using WS.Chat;
using WS.Editions;
using WS.Employees;
using WS.Employees.Configuration;
using WS.Friendships;
using WS.MultiTenancy;
using WS.MultiTenancy.Accounting;
using WS.MultiTenancy.Payments;
using WS.Storage;
using WS.Tasks;
using WS.Tasks.Configuration;

namespace WS.EntityFrameworkCore
{
    public class WSDbContext : AbpZeroDbContext<Tenant, Role, User, WSDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public WSDbContext(DbContextOptions<WSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            TaskEntityConfiguration.Configure(modelBuilder.Entity<Task>());
            EmployeeEntityConfiguration.Configure(modelBuilder.Entity<Employee>());

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
