using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstAgenda.IdentityServer.Core
{
    public interface IHomeRealmDiscoveryService
    {
        Task<HomeRealmDiscoveryInfo> GetInfo(string loginId);
    }

    public class HomeRealmDiscoveryService : IHomeRealmDiscoveryService
    {
        private readonly FirstAgendaIdentityStoreContext _dbContext;

        public HomeRealmDiscoveryService(FirstAgendaIdentityStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<HomeRealmDiscoveryInfo> GetInfo(string loginId)
        {
            if (string.IsNullOrEmpty(loginId)) return null;
            if (!loginId.Contains('@')) return null;

            var domain = loginId.Substring(loginId.IndexOf('@'));
            if (string.IsNullOrEmpty(domain)) return null;

            var hr = await _dbContext.ExternalProviders.SingleOrDefaultAsync(hr => hr.HomeRealm == domain);
            if (hr == null) return null;

            return new HomeRealmDiscoveryInfo()
            {
                ExternalProviderId = hr.ProviderId
            };
        }
    }

    public class HomeRealmDiscoveryInfo
    {
        public string ExternalProviderId { get; set; }
        
    }
}