using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstAgenda.IdentityServer.Core
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<FirstAgendaIdentityStoreContext>
    {
        public FirstAgendaIdentityStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FirstAgendaIdentityStoreContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=IdentityPlatform;User Id=sa;Password=S0me_Passw0rd;");

            return new FirstAgendaIdentityStoreContext(optionsBuilder.Options);
        }
    }
}