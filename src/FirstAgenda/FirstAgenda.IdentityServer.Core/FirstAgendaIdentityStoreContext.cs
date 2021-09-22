using FirstAgenda.IdentityServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAgenda.IdentityServer.Core
{
    public class FirstAgendaIdentityStoreContext : DbContext
    {
        public FirstAgendaIdentityStoreContext(DbContextOptions<FirstAgendaIdentityStoreContext> options) :
            base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ExternalProvider> ExternalProviders { get; set; }
        public DbSet<AccountProfile> AccountProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAccount(modelBuilder);
            ConfigureAccountProfile(modelBuilder);
            ConfigureExternalProvider(modelBuilder);
        }

        private static void ConfigureAccountProfile(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AccountProfile>()
                .ToTable("AccountProfile")
                .HasKey(k => k.Id)
                ;
        }

        private static void ConfigureExternalProvider(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ExternalProvider>()
                .ToTable("ExternalProvider")
                .HasKey(k => k.Id)
                ;
        }

        private static void ConfigureAccount(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Account>()
                .ToTable("Account")
                .HasKey(k => k.Id)
                ;

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountProfiles).WithOne(p => p.Account).IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                ;
        }
    }
}