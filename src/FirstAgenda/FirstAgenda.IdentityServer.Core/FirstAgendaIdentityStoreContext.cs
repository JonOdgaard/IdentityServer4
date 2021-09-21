using FirstAgenda.IdentityServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAgenda.IdentityServer.Core
{
    public class FirstAgendaIdentityStoreContext : DbContext
    {
        public FirstAgendaIdentityStoreContext(DbContextOptions<FirstAgendaIdentityStoreContext> options) : base(options)
        {
        }

        public DbSet<FirstAgendaAccount> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<FirstAgendaAccount>()
                .ToTable("Account")
                .HasKey(k => k.Id)
                ;
        }
    }
}